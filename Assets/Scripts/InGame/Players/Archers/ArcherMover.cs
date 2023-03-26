using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Archers
{
    public class ArcherMover : PlayerMover
    {
        private ArcherAnimationPlayer archerAnimationPlayer;

        public override void Init(PlayerParameter playerParameter)
        {
            base.Init(playerParameter);

            archerAnimationPlayer = playerAnimationPlayer as ArcherAnimationPlayer;
            if (archerAnimationPlayer == null)
            {
                Debug.LogError("AnimationPlayer‚ÌŒ^‚ªŠÔˆá‚Á‚Ä‚¢‚Ü‚·");
            }
        }

        public override void Move(Vector3 moveVec)
        {
            if (rigidbody == null)
                return;

            //if (archerAnimationPlayer.IsLanding)
            //    return;

            if (archerAnimationPlayer.currentAttackState == PlayerAttackStateType.Special)
                return;

            if (archerAnimationPlayer.IsAiming)
            {
                var moveSpeed = (playerParameter.baseMoveSpeed + playerParameter.addMoveSpeed) * playerParameter.moveSpeedMagnification * 0.9f;
                var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
                rigidbody.velocity = velocity;

                archerAnimationPlayer.PlayAimWalkAnimation(moveVec.x, moveVec.z);
            }
            else
            {
                var moveSpeed = (playerParameter.baseMoveSpeed + playerParameter.addMoveSpeed) * playerParameter.moveSpeedMagnification;
                var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
                rigidbody.velocity = velocity;
                transform.LookAt(transform.position + moveVec);

                if (Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.z, 0))
                {
                    archerAnimationPlayer.StopRunAnimation();
                }
                else
                {
                    archerAnimationPlayer.PlayRunAnimation();
                }
            }
        }
    }
}

