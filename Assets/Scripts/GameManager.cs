using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int MinGames;
    [SerializeField] private LevelData[] allLevels;

    public static event Action StartGame;
    public static event Action EndGame;
    public static GameManager Instance { get; private set; }

    private LevelData currentLevel;
    private List<LevelData> availableLevels;
    private List<PrePlayer> registeredPlayers = new List<PrePlayer>();

    #region Getters & Setters

    public LevelData _CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGameEvent()
    {
        StartGame?.Invoke();
    }
    private void InitializeLevels()
    {
        if (allLevels != null && allLevels.Length > 0)
        {
            availableLevels = new List<LevelData>(allLevels);
        }
        else
        {
            availableLevels = new List<LevelData>();
        }
    }
    public void SelectLevel()
    {
        SelectionLevelRandom();
        Debug.Log(currentLevel.scene_name);
    }
    private LevelData SelectionLevelRandom()
    {
        if (availableLevels == null || availableLevels.Count == 0)
        {
            Debug.LogWarning("[GameManager] No hay niveles disponibles o ya se han jugado todos.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableLevels.Count);
        currentLevel = availableLevels[randomIndex];

        availableLevels.RemoveAt(randomIndex);

        return currentLevel;
    }

    public void RegisterPlayer(PrePlayer player)
    {
        if (player != null && !registeredPlayers.Contains(player))
        {
            registeredPlayers.Add(player);
        }
    }

    public void UnregisterPlayer(PrePlayer player)
    {
        if (player != null && registeredPlayers.Contains(player))
        {
            registeredPlayers.Remove(player);
        }
    }
    private bool AreAllPlayersReady()
    {
        if (registeredPlayers == null || registeredPlayers.Count == 0)
        {
            return false;
        }

        foreach (PrePlayer player in registeredPlayers)
        {
            if (!player._IsReady)
            {
                return false;
            }
        }

        return true;
    }
    public void PlayInteractable()
    {
        UIManager.Instance._PlayButton.interactable = AreAllPlayersReady();
    }
}