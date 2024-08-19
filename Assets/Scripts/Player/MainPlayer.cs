using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace HorroHouse.Player
{
    public class MainPlayer : MonoBehaviour
    {
        #region STATES
        Base _currentState;
        public IDLE IDLE;
        public SPRINT SPRINT;

        #endregion

        [Range(0, 1)] public float playerSpeedDamp = 0.1f;
        [Range(0, 1)] public float turnSmoothDamp = 0.1f;

        [Space(10)]
        public CharacterController controller;
        public Animator anim;
        public Transform cameraTransform;
        public Rigidbody rb;

        [Space(10)]
        public float mouseSensitivity = 1f;
        public float currentStamina = 10;
        public float staminaSpeed = 5;
        public float playerSpeed = 15;
        public float sprintSpeedMultiplier = 4;
        public float gravityMultiplier = 3.0f;


        [Space(10)]
        public bool isCooldown;
        public bool isStaminaCoolDown = false;
        public bool isUsableStaminaRestored = false;
        public bool isInCutScene = false;
        public bool isDead;
        public bool isInLift = false;

        
        //[Space(10)]
        //public PlayerStats stats;
        private void Start()
        {
            StateInitialize();
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void StateInitialize()
        {
            IDLE = new IDLE(this);
            SPRINT = new SPRINT(this);
            _currentState = IDLE;
            _currentState.EnterState();
        }
        private void Update()
        {
            _currentState.ManageInput();
            _currentState.LogicUpdateState();
        }

        public void ChangeCurrentState(Base newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void Cooldown()
        {
            isCooldown = true;
        }
    }
}
