using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject Panel_Game;
    [SerializeField] private GameObject Panel_Results;
    [SerializeField] private GameObject Panel_FinalResults;
    [SerializeField] private GameObject Panel_Countdown;
    [Header("Buttons")]
    [SerializeField] private Button playButton;

    [SerializeField] private List<GameObject> panels;

    public static UIManager Instance { get; private set; }
    
    #region Getters & Setters
    public Button _PlayButton
    {
        set { playButton = value; }
        get { return playButton; }
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
    #region Buttons
    public void ButtonPlay()
    {
        GameManager.Instance.StartGameEvent();
    }
    public void ButtonsChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ButtonOnPanel(GameObject p)
    {
        p.SetActive(true);
    }
    public void ButtonOffPanel(GameObject p)
    {
        p.SetActive(false);
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }
    
    #endregion
}