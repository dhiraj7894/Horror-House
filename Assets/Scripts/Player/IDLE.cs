using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  


namespace HorroHouse.Player
{
    public class IDLE : Base
    {
        public IDLE(MainPlayer _Player) : base(_Player)
        {
            player = _Player;
        }

        public override void EnterState()
        {
            base.EnterState();
            _input = Vector2.zero;
            _isSprint = false;
            _isCrouch = false;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
            if (!_isDead)
            {
                player.anim.SetFloat(AnimHash.SPEED, _input.magnitude);
                if (_input.magnitude >= 0.1f)
                {
                    MovementUpdate();
                }
                if (_isSprint)
                {
                    player.ChangeCurrentState(player.SPRINT);
                }
            }
            else
            {
                //player.anim.SetFloat(AnimHash.SPEED, 0, player.playerSpeedDamp, Time.deltaTime);
            }
            crouchMovement(InputActions._crouchAction.IsPressed());
        }

        public override void ManageInput()
        {
            base.ManageInput();
            _input = InputActions._moveAction.ReadValue<Vector2>();

        }
    }
}
