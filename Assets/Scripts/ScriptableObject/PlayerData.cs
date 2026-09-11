using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "NewPlayer", menuName = "GameOS/Player Data")]

public class PlayerData : ScriptableObject
{
    [Header("Properties")]
    public string name_player;
    public float score;
    public Color color;
    [Space]
    public int index;
    public InputDevice device;
    public void ResetData(int defaultIndex)
    {
        index = defaultIndex;
        score = 0;
        device = null;
    }
}
