using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    public static InputAction _moveAction;
    public static InputAction _mouseAction;
    public static InputAction _dashAction;
    public static InputAction _jumpAction;
    public static InputAction _crouchAction;


    public static InputAction _attack;
    public static InputAction _heavyAttack;

    public static InputAction _specialAttackA;
    public static InputAction _specialAttackB;

    public static InputAction _shieldAction;

    public static InputAction _pressF;
    public static InputAction _drop;
    public static InputAction _submit;
    public static InputAction _lockOn;


    public PlayerInput playerInput;

    private void Start()
    {
        _moveAction = playerInput.actions["Move"];
        _mouseAction = playerInput.actions["Mouse"];
        _jumpAction = playerInput.actions["Jump"];
        _dashAction = playerInput.actions["Dash"];
        _crouchAction = playerInput.actions["Crouch"];

        _attack = playerInput.actions["Attack"];
        _heavyAttack = playerInput.actions["HeavyAttack"];

        _specialAttackA = playerInput.actions["MiniSAttack"];
        _specialAttackB = playerInput.actions["UltimateAttack"];

        _shieldAction = playerInput.actions["Shield"];
        _pressF = playerInput.actions["PressF"];
        _drop = playerInput.actions["Drop"];
        _submit = playerInput.actions["Submit"];
        _lockOn = playerInput.actions["LockOn"];
    }

    private void Update()
    {
        if (_pressF.triggered)
        {
            EventManager.Instance.PressedFButton();
        }

         if (_drop.triggered)
        {
            EventManager.Instance.PressedGButton();
        }
    }
}
