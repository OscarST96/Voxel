using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbySystem : MonoBehaviour
{
    [Header("Configuración de Spawn y Datos")]
    [SerializeField] private Transform[] spawn_point;
    [SerializeField] private PlayerData[] playerDataArray;

    private List<PlayerInput> connectedPlayers = new List<PlayerInput>();

    public void OnPlayerJoinedEvent(PlayerInput playerInput)
    {
        connectedPlayers.Add(playerInput);
        int playerIndex = playerInput.playerIndex;

        HandleSpawn(playerInput, playerIndex);
        HandlePlayerData(playerInput, playerIndex);
    }

    public void OnPlayerLeftEvent(PlayerInput playerInput)
    {
        if (connectedPlayers.Contains(playerInput))
        {
            PrePlayer prePlayer = playerInput.GetComponent<PrePlayer>();
            if (prePlayer != null && GameManager.Instance != null)
                GameManager.Instance.UnregisterPlayer(prePlayer);

            connectedPlayers.Remove(playerInput);
            Debug.Log($"Un jugador ha abandonado la partida. Jugadores restantes: {connectedPlayers.Count}");
        }
    }

    private void HandleSpawn(PlayerInput playerInput, int playerIndex)
    {
        if (spawn_point != null && spawn_point.Length > playerIndex && spawn_point[playerIndex] != null)
        {
            playerInput.transform.position = spawn_point[playerIndex].position;
            playerInput.transform.rotation = spawn_point[playerIndex].rotation;
        }
        else
            Debug.LogWarning($"[LobbySystem] No hay suficientes 'spawn_points' configurados para el jugador {playerIndex + 1}.");
    }

    private void HandlePlayerData(PlayerInput playerInput, int playerIndex)
    {
        if (playerDataArray != null && playerDataArray.Length > playerIndex && playerDataArray[playerIndex] != null)
        {
            PrePlayer prePlayer = playerInput.GetComponent<PrePlayer>();

            if (prePlayer != null)
            {
                PlayerData assignedData = playerDataArray[playerIndex];

                if (playerInput.devices.Count > 0)
                    assignedData.device = playerInput.devices[0];

                prePlayer._PlayerData = assignedData;

                if (GameManager.Instance != null)
                    GameManager.Instance.RegisterPlayer(prePlayer);

                Debug.Log($"[LobbySystem] PlayerData '{assignedData.name_player}' asignado al Jugador {playerIndex + 1}.");
            }
            else
                Debug.LogError($"[LobbySystem] El prefab del jugador no contiene el componente PrePlayer.", playerInput);
        }
        else
            Debug.LogWarning($"[LobbySystem] No hay suficientes elementos en 'playerDataArray' para asignar al jugador {playerIndex + 1}.");
    }
}