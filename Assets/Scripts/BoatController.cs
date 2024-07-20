using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : Boat
{
    public enum Player
    {
        Player1, Player2
    }

    [Header("Controls")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction movementAction;
    [SerializeField] private InputAction passWeaponAction;
    [SerializeField] private Player player;


    [SerializeField] private float speed;
    [SerializeField] private Vector2 inputVector;




    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        playerInput.Enable();
        movementAction = player == Player.Player1 ? playerInput.Player1.Movement : playerInput.Player2.Movement;
        passWeaponAction = player == Player.Player1 ? playerInput.Player1.Movement : playerInput.Player2.Movement;

        movementAction.performed += MovementAction_performed;
        movementAction.canceled += MovementAction_canceled;
    }

    private void MovementAction_canceled(InputAction.CallbackContext context)
    {
        inputVector = Vector2.zero;
    }

    private void MovementAction_performed(InputAction.CallbackContext obj)
    {
        
        inputVector = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.AddForce(inputVector * speed * Time.deltaTime, ForceMode2D.Force);
        
    }
}
