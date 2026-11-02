using UnityEngine;
using UnityEngine.EventSystems;

public class UIGamePadNavigations : MonoBehaviour
{
    [Tooltip("El primer botón que debe seleccionarse al abrir este panel")]
    [SerializeField] private GameObject firstSelectedButton;

    private void OnEnable()
    {
        SelectFirstButton();
    }

    public void SelectFirstButton()
    {
        if (firstSelectedButton != null && EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }
}
