using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Puntuación por posición (GDD)")]
    [SerializeField] private int[] pointsPerPosition = new int[] { 100, 70, 40, 20 };

    private int playerCount;
    private int[] totalScores;
    private int[] roundScores;
    private float[] survivalTimes;
    private int currentRound = 0;
    private int maxRounds = 3;

    public int CurrentRound => currentRound;
    public int MaxRounds => maxRounds;
    public int PlayerCount => playerCount;

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

    public void Initialize(int count)
    {
        playerCount = count;
        totalScores = new int[4];
        roundScores = new int[4];
        survivalTimes = new float[4];
        currentRound = 0;
        
        for (int i = 0; i < 4; i++)
        {
            totalScores[i] = 0;
            roundScores[i] = 0;
            survivalTimes[i] = 0f;
        }
    }

    public void SetRoundScores(int[] positions, float[] times = null)
    {
        for (int i = 0; i < playerCount; i++)
        {
            int pos = positions[i];
            if (pos >= 1 && pos <= 4)
            {
                roundScores[i] = pointsPerPosition[pos - 1];
            }
            if (times != null && times[i] > 0)
            {
                survivalTimes[i] += times[i];
            }
        }
    }

    public void CommitRoundScores()
    {
        for (int i = 0; i < playerCount; i++)
        {
            totalScores[i] += roundScores[i];
            roundScores[i] = 0;
        }
        currentRound++;
    }

    public void ResetRoundScores()
    {
        for (int i = 0; i < playerCount; i++)
        {
            roundScores[i] = 0;
        }
    }

    public int GetTotalScore(int playerIndex) => totalScores[playerIndex];
    public int GetRoundScore(int playerIndex) => roundScores[playerIndex];
    public float GetSurvivalTime(int playerIndex) => survivalTimes[playerIndex];
    public int[] GetTotalScores() => totalScores;
    public int[] GetRoundScores() => roundScores;
    public float[] GetSurvivalTimes() => survivalTimes;

    public int[] GetRanking()
    {
        int[] indices = new int[playerCount];
        for (int i = 0; i < playerCount; i++) indices[i] = i;
        
        System.Array.Sort(indices, (a, b) => 
        {
            int scoreCompare = totalScores[b].CompareTo(totalScores[a]);
            if (scoreCompare != 0) return scoreCompare;
            return survivalTimes[b].CompareTo(survivalTimes[a]);
        });
        
        return indices;
    }

    public int[] GetRoundRanking()
    {
        int[] indices = new int[playerCount];
        for (int i = 0; i < playerCount; i++) indices[i] = i;
        
        System.Array.Sort(indices, (a, b) => roundScores[b].CompareTo(roundScores[a]));
        return indices;
    }

    public bool IsFinalRound() => currentRound >= maxRounds;
    public void SetMaxRounds(int rounds) => maxRounds = rounds;
    public int GetPointsForPosition(int position) => (position >= 1 && position <= 4) ? pointsPerPosition[position - 1] : 0;

    public void ResetAll()
    {
        Initialize(playerCount);
    }
}