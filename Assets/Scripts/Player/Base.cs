using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HorroHouse.Player
{
    public abstract class Base
    {
        public MainPlayer player;
        protected Vector2 _input;


        protected bool _isIdle = false;
        protected bool _isSprint = false;
        protected bool _isCrouch = false;
        protected bool _isDash = false;

        public bool _isDead = false;
        public bool _isRevived = false;

        protected float _playerSpeed = 5;

        private float _turnSmoothVelocity;
        private float _gravity = -9.8f;
        private float _gravityMulitplier = 3.0f;

        private float _strafeSpeedMultiplier = 0.75f;
        private float _backSpeedMultiplier = 0.4f;
        protected Vector3 _velocity;

        public Base(MainPlayer mainPlayer)
        {
            player = mainPlayer;
        }
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void ManageInput() { }
        public virtual void LogicUpdateState()
        {
            MouseLook();
            if(!player.isInLift)addGeavity();
        }

        Vector3 moveDir;
        public void MovementUpdate(float speed = 1)
        {
            if (!_isDead)
            {
                moveDir = player.transform.right * _input.x + player.transform.forward * _input.y;
                player.controller.Move(moveDir * _playerSpeed * speed * Time.deltaTime);
            }
        }

        float xRotation = 0;
        public void MouseLook()
        {
            Vector2 mouse = InputActions._mouseAction.ReadValue<Vector2>();
            float mouseX = mouse.x * player.mouseSensitivity * Time.deltaTime;
            float mouseY = mouse.y * player.mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -50, 50);


            player.cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            player.transform.Rotate(Vector3.up * mouseX);
        }
        public void addGeavity()
        {
            if (player.controller.isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -1f;
            }
            else
            {
                _velocity.y += _gravity * _gravityMulitplier * Time.deltaTime;
            }

            player.controller.Move(_velocity * Time.deltaTime);
        }
        public void crouchMovement(bool isTrue)
        {
            // Set controller center height and player graphic position

            if (isTrue)
            {
                player.controller.center = new Vector3(0, 0.3f, 0);
                player.controller.height = .7f;
                player.playerGFXTransform.localPosition = new Vector3(0, -0.636f, 0);
                _playerSpeed = player.playerSpeed / (1.3f);
            }
            else
            {
                player.controller.center = new Vector3(0, 0.85f, 0);
                player.controller.height = 1.7f;
                player.playerGFXTransform.localPosition = new Vector3(0, 0, 0);
                _playerSpeed = player.playerSpeed * (1.3f);
            }
            

        }
    }

}
