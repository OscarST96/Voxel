using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //-> prefab del player
    [SerializeField] private List<PlayerData> so_players;
    [SerializeField] private List<Transform> pos_spawn;
    private void Start()
    {
        SpawnerPlayers();
    }
    private void SpawnerPlayers()
    {
        for (int i = 0; i < GameManager.Instance.Count_Players; i++)
        {
            PlayerInput p_input = PlayerInput.Instantiate(prefab, playerIndex: so_players[i].index, pairWithDevice: so_players[i].device);

            p_input.transform.position = pos_spawn[i].position;
            p_input.transform.SetPositionAndRotation(pos_spawn[i].position,pos_spawn[i].rotation);
            p_input.GetComponent<PlayerController>().SO_PLAYER = so_players[i];
            print("Se creo un player");
        }
    }
}
