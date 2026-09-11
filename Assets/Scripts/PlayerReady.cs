using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReady : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Ready(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        GameManager.Instance.SetPlayerReady(playerInput.playerIndex);

    }
}
