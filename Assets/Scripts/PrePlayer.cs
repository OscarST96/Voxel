using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrePlayer : MonoBehaviour
{
    public MeshFilter[] mesh_character;
    [SerializeField] private TextMeshProUGUI text_name;
    private MeshFilter targetMeshFilter;
    private PlayerData playerData;
    private int currentIndex = 0;
    private bool isReady = false;

    public PlayerData _PlayerData
    {
        get { return playerData; }
        set { playerData = value; }
    }
    public bool _IsReady
    {
        get { return isReady; }
        set { isReady = value; }
    }
    void Start()
    {
        targetMeshFilter = GetComponent<MeshFilter>();

        if (targetMeshFilter == null)
        {
            targetMeshFilter = gameObject.AddComponent<MeshFilter>();
            Debug.Log($"[PrePlayer] No se encontró un MeshFilter en {gameObject.name}. Se ha añadido uno automáticamente.", this);
        }
        UpdateCharacterDisplay();
        AssingPlayerData();
    }

    void Update()
    {

    }

    public void AssingPlayerData()
    {
        if (playerData == null) return;

        GetComponent<Renderer>().material.color = playerData.color;
        text_name.text = playerData.name_player;
    }

    private void UpdateCharacterDisplay()
    {
        if (mesh_character == null || mesh_character.Length == 0) return;

        if (mesh_character[currentIndex] != null && mesh_character[currentIndex].sharedMesh != null)
            targetMeshFilter.sharedMesh = mesh_character[currentIndex].sharedMesh;
        else
            Debug.LogWarning($"[PrePlayer] El MeshFilter en el índice {currentIndex} está vacío o no tiene mesh asignado.", this);
    }

    #region Inputs
    public void SelectionCharacter(InputAction.CallbackContext axis)
    {
        if (!axis.performed) return;

        if (mesh_character == null || mesh_character.Length == 0) return;

        float inputVal = axis.ReadValue<float>();

        if (inputVal > 0)
            currentIndex = (currentIndex + 1) % mesh_character.Length;
        else if (inputVal < 0)
            currentIndex = (currentIndex - 1 + mesh_character.Length) % mesh_character.Length;

        UpdateCharacterDisplay();
    }

    public void IsReady(InputAction.CallbackContext button)
    {
        if (!button.performed) return;

        isReady = !isReady;
        GameManager.Instance.PlayInteractable();
    }
    #endregion
}