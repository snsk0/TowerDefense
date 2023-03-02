using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;

        private PlayerParameter playerParameter;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public void Move(Vector3 moveVec)
        {
            if (rigidbody == null)
                return;

            if (playerAnimationPlayer.IsLanding)
                return;

            //çUåÇíÜÇÕìÆÇ©Ç»Ç¢ÇÊÇ§Ç…
            if (playerAnimationPlayer.IsAttackMotion)
            {
                rigidbody.velocity = Vector3.zero;
                playerAnimationPlayer.StopRunAnimation();
                return;
            }

            var moveSpeed = (playerParameter.baseMoveSpeed + playerParameter.addMoveSpeed) * playerParameter.moveSpeedMagnification;
            var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.velocity = velocity;
            transform.LookAt(transform.position+moveVec);

            if(Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.z, 0))
            {
                playerAnimationPlayer.StopRunAnimation();
            }
            else
            {
                playerAnimationPlayer.PlayRunAnimation();
            }
        }
    }
}

