using HorroHouse.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CaretakerManager : MonoBehaviour
{
    #region STATES
    CaretakerBase _currentState;
    public CaretakerIdle IDLE;
    public CaretakerMovement MOVEMENT;
    public CaretakerAction ACTION;
    #endregion
    public Animator anim;
    public NavMeshAgent agent;

    [Space(5)]
    public float speed;
    private void Start()
    {
        StateInitialize();
    }
    public void StateInitialize()
    {
        IDLE = new CaretakerIdle(this);
        MOVEMENT = new CaretakerMovement(this);
        ACTION = new CaretakerAction(this);
        _currentState = IDLE;
        _currentState.EnterState();
    }

    public void ChangeCurrentState(CaretakerBase newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    private void Update()
    {        
        _currentState.LogicUpdateState();
    }
}
