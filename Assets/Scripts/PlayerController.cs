using UnityEngine;
using UnityEngine.InputSystem;

//La idea es que se mueva con el mando o teclado y que hacia donde se mueva sea a donde mire
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField,Min(0.1f)] private float moveSpeed = 7.5f;
    [SerializeField,Min(0.1f)] private float forceJump = 5;
    [Header("scripts & scriptableObjects")]
    [SerializeField] private PlayerData so_player;
    [Header("Components")]
    [SerializeField] private GameObject mesh_player;

    private Rigidbody rb;
    private InputDevice input_device;
    private Vector2 inputMov;
    private PlayerInput _playerInput;
    #region Setters & Getters
    public PlayerData SO_PLAYER
    {
        get { return so_player; }
        set { so_player = value; }
    }
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        AssingValueSO();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("Player");
        rb.useGravity = true;
    }
    private void FixedUpdate()
    {
        MovToPlayer();
    }
    private void MovToPlayer()
    {
        Vector3 mov = new Vector3(inputMov.x * moveSpeed, rb.linearVelocity.y, inputMov.y * moveSpeed);
        rb.linearVelocity = mov;
    }
    private void JumpToPlayer()
    {
        rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
    }
    private void PushToPlayer()
    {
        //Agregar mecanica de empuje
    }
    private void AssingValueSO()//-> Asignaciones del scriptableObject
    {
        mesh_player.GetComponent<MeshRenderer>().material.color = so_player.color;
        input_device = so_player.device;
    }
    #region InputActions
    public void Movement(InputAction.CallbackContext context)//GamePad y Keyboard
    {
        inputMov = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)//Button
    {
        if (context.started)
        {
            JumpToPlayer();
        }
    }
    public void Push(InputAction.CallbackContext context)//Button
    {

    }
    #endregion
}
