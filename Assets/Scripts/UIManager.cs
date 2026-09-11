using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Play")]
    [SerializeField] private GameObject Main_Panel;
    [SerializeField] private GameObject Players_Panel;
    [Header("Ready")]
    [SerializeField] private GameObject[] Panel_Ps;
    [Header("Options")]
    [SerializeField] private GameObject Options_Panel;
    [Header("Game")]
    [SerializeField] private GameObject Game_Panel;

    [SerializeField] private List<GameObject> panels;
    //singleton
    public static UIManager Instance { get; private set; }
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
    private void OnEnable()
    {
        GameManager.StartGame += ChangeScene;
    }

    // SIEMPRE desuscribirse al desactivar para evitar fugas de memoria
    private void OnDisable()
    {
        GameManager.StartGame -= ChangeScene;
    }
    void Start()
    {
    }
    public void ButtonOnPanel(GameObject p)
    {
        p.SetActive(true);
    }
    public void ButtonOffPanel(GameObject p)
    {
        p.gameObject.SetActive(false);
    }
    private void OffPanelPs()
    {
        for(int i = 0; i < Panel_Ps.Length; i++)
        {
            Panel_Ps[i].SetActive(false);
        }
    }
    public void ButtonReady(int id)
    {
        GameManager.Instance.SetPlayerReady(id);
    }
    public void AllOffPanels()
    {
        for (int i = 0;i < panels.Count; i++)
        {
            panels[i].SetActive(false);
        }
    }
    public void OnPanelsPlayer(int index)
    {
        if (Panel_Ps[index] == null) return;
        Panel_Ps[index].SetActive(true);
    }
    public void OffPanelsPlayer(int index)
    {
        Panel_Ps[index].SetActive(false);
    }
    private void ChangeScene()
    {
        AllOffPanels();
        Game_Panel.SetActive(true);
    }
    
    public void ButtonExitGame()
    {
        Application.Quit();
    }
}
