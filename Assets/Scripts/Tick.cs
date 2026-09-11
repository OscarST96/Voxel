using TMPro;
using UnityEngine;

public class Tick : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_Time;
    public LevelData so_level;
    private float currentTime;
    private float t = 0;
    private bool isStar = false;
    private void OnEnable()
    {
        GameManager.StartGame += StartTimer;
    }
    private void OnDisable()
    {
        GameManager.StartGame -= StartTimer;
    }
    void Start()
    {
        /*if (so_level != null)
            currentTime = so_level.timer;.*/
        UIUpdateTextTime();
    }
    void Update()
    {
        if (!isStar) return;

        t += Time.deltaTime;
        if (t >= 1f)
        {
            currentTime--;
            t = 0f;
            
            UIUpdateTextTime();
            if (currentTime <= 0)
            {
                StopTimer();
                GameManager.Instance.EventEndGame();
            }
        }
    }
    private void StopTimer()
    {
        isStar = false;
    }
    private void StartTimer()
    {
        if (so_level == null) return;
        currentTime = so_level.timer;
        t = 0f;
        isStar = true;
        UIUpdateTextTime();
    }
    public void SetLevelData(LevelData data)
    {
        so_level = data;
    }
    private void UIUpdateTextTime()
    {
        text_Time.SetText("{0}", currentTime);
    }
}
