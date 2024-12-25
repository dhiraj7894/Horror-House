using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Windows;
using UnityEngine.Animations.Rigging;

namespace HorroHouse.Player
{
    public class MainPlayer : MonoBehaviour
    {
        #region STATES
        Base _currentState;
        public IDLE IDLE;
        public SPRINT SPRINT;

        #endregion

        #region Player Settings
        [Header("Movement Settings")]
        [Range(0, 1)] public float playerSpeedDamp = 0.1f; // Damping factor for player speed
        [Range(0, 1)] public float turnSmoothDamp = 0.1f; // Damping factor for smooth turning
        public float mouseSensitivity = 1f; // Mouse sensitivity for camera movement
        public float playerSpeed = 15; // Base speed of the player
        public float sprintSpeedMultiplier = 4; // Speed multiplier when sprinting
        public float gravityMultiplier = 3.0f; // Multiplier for gravity effect

        [Header("Stamina Settings")]
        public float currentStamina = 10; // Current stamina level of the player
        public float staminaSpeed = 5; // Rate at which stamina is consumed or recovered

        [Header("Head Rotation Limits")]
        public float maxHeadRotation = 50; // Maximum head rotation angle upwards
        public float minHeadRotation = -50; // Maximum head rotation angle downwards
        #endregion

        #region References
        [Header("Player References")]
        public CharacterController controller; // Reference to the character controller component
        public ControllerPlayer playerController; // Custom controller script for handling player input
        public Animator anim; // Animator for controlling player animations
        public Transform cameraTransform; // Transform of the player camera
        public GameObject playerCamera; // Main camera object
        public Transform playerGFXTransform; // Transform for the player graphical representation
        public Rigidbody rb; // Rigidbody for physics interactions
        public GameObject torch; // Reference to the torch object
        public RigBuilder rigbuilder; // IK constraint for the rigbuilder
        #endregion

        #region Player State Flags
        [Header("State Flags")]
        public bool isCooldown; // Indicates if the player is in cooldown state
        public bool isStaminaCoolDown; // Indicates if stamina is in cooldown
        public bool isUsableStaminaRestored; // Indicates if usable stamina has been restored
        public bool isInCutScene; // Indicates if the player is in a cutscene
        public bool isDead; // Indicates if the player is dead
        public bool isInLift; // Indicates if the player is inside a lift
        public bool isHideden; // Indicates if the player is hidden
        #endregion


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
            if (GameManager.Instance.isCutScenePlaying || isHideden || isDead)
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

        public void CharacterStands()
        {
            LeanTween.delayedCall(.5f, () => EventManager.Instance.eventForTask.CutSceneCompleted?.Invoke());
            rigbuilder.layers[0].active = true;
            torch.SetActive(true);
            Debug.Log($"Character stand completed : {torch.activeSelf}");
        }

        public void TwoBoneIKWeight()
        {
            Debug.Log("TwoBoneIKWeight");
            torch.SetActive(false);
            rigbuilder.layers[0].active = false;
        }
    }
}
