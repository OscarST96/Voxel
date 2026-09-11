using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbySystem : MonoBehaviour
{
    public PlayerData[] players;
    private void Awake()
    {
        // Limpiamos los datos del ScriptableObject al arrancar la escena
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
                players[i].ResetData(i);
        }
    }
    // Esta función se ejecutará automáticamente cada vez que se una un jugador || cuando alcanza el maxima de jugadores, se bloquea solo
    public void OnPlayerJoinedEvent(PlayerInput playerInput)
    {
        int i = playerInput.playerIndex;
        if (i < players.Length)
        {
            GameManager.Instance.Count_Players++;
            players[i].device = playerInput.devices[0]; //-> Asignamos su input.
            UIManager.Instance.OnPanelsPlayer(playerInput.playerIndex);
            Debug.Log("Connect: " + i);
        }
    }

    // Esta función se ejecutará cuando un jugador abandone la partida
    public void OnPlayerLeftEvent(PlayerInput playerInput)
    {
        //GameManager.Instance.Count_Players--;
        UIManager.Instance.OffPanelsPlayer(playerInput.playerIndex);
        Debug.Log("Disconnet: " + playerInput.playerIndex);
    }
    // En GameManager.cs o LobbySystem.cs
    /*public int GetActivePlayerCount()
    {
        int count = 0;
        foreach (var player in players)
        {
            if (player != null && player.device != null)
                count++;
        }
        return count;
    }*/
}
