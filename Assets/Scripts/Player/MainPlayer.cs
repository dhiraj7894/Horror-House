using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;

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
        public ControllerPlayer playerController;
        public Animator anim;
        public Transform cameraTransform;
        public GameObject playerCamera;
        public Transform playerGFXTransform;
        public Rigidbody rb;
        public GameObject torch;
        

        [Space(10)]
        public float mouseSensitivity = 1f;
        public float currentStamina = 10;
        public float staminaSpeed = 5;
        public float playerSpeed = 15;
        public float sprintSpeedMultiplier = 4;
        public float gravityMultiplier = 3.0f;
        public float maxHeadRotation = 50;
        public float minHeadRotation = -50;


        [Space(10)]
        public bool isCooldown;
        public bool isStaminaCoolDown = false;
        public bool isUsableStaminaRestored = false;
        public bool isInCutScene = false;
        public bool isDead;
        public bool isInLift = false;
        public bool isHided = false;

        
        //[Space(10)]
        //public PlayerStats stats;
        private void Start()
        {
            StateInitialize();            
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
            if (GameManager.Instance.isCutScenePlaying || isHided)
            {
                anim.SetFloat(AnimHash.SPEED, 0);
                return;
            }
                
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
