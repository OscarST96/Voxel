using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//Necesito obtener la cantidad de jugadores
public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelData[] so_level;

    [Header("Settings")]
    [SerializeField][Tooltip("The value must be less than the number of minigames.")] private int MaxMiniGames = 3;

    [Header("Scripts")]
    [SerializeField] private Tick tick;//<- Agregar desde el inspector

    private List<LevelData> games_select = new List<LevelData>();
    private int count_players; //Importante
    private bool[] ready = new bool[4];

    public static event Action StartGame;
    public static event Action EndGame;
    public static GameManager Instance { get; private set; }
    #region Getters&Setters
    public int Count_Players
    {
        get { return count_players; }
        set { count_players = value; }
    }
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    private void Update()
    {
        print(count_players);
    }
    public void SetPlayerReady(int id_player)
    {
        if (!ready[id_player])
            ready[id_player] = true;
        else 
            ready[id_player] = false;

        AllPlayersReady();
    }
    private void AllPlayersReady()
    { 
        int game_ready = 0;
        if (count_players == 0) return;

        for (int i = 0; i < count_players; i++)
        {
            if (ready[i]) game_ready++;
        }

        if (game_ready == count_players)
        {
            //for (int i = 0; i < ready.Length; i++) ready[i] = false;

            SelecMiniGame();
        }
    }
    public void SelecMiniGame()
    {
        if (so_level == null) return;

        if (games_select.Count != 0)
            RemoveGamesIndex();

        for (int i = 0; i < MaxMiniGames; i++)
        {
            int index = UnityEngine.Random.Range(0, so_level.Length);

            if (games_select.Contains(so_level[index]))
            {
                i--;
                continue;
            }
            games_select.Add(so_level[index]);
        }
        tick.SetLevelData(games_select[0]);
        ChangeSceneSelect();
    }
    public void NextScene() => ChangeSceneSelect(); //Para buttons
    private void ChangeSceneSelect()
    {
        PlayerInputManager pManager = PlayerInputManager.instance;
        if (pManager != null)
        {
            pManager.DisableJoining();
        }
        string l_Select = GetNameSceneGame();
        EventStartGame();
        SceneManager.LoadScene(l_Select);
    }
    private string GetNameSceneGame()
    {
        string n_level = games_select[0].scene_name;
        games_select.Remove(games_select[0]);
        return n_level;
    }
    public void ResetReadyState()
    {
        for (int i = 0; i < count_players; i++) // Solo jugadores reales
            ready[i] = false;
    }

    public void RemoveGamesIndex()
    {
        games_select.Clear();
    }
    public void EventStartGame()
    {
        StartGame?.Invoke();
    }
    public void EventEndGame()
    {
        EndGame?.Invoke();
    }
}
