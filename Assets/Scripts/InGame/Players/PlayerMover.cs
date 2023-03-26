using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rigidbody;
        [SerializeField] protected PlayerAnimationPlayer playerAnimationPlayer;

        protected PlayerParameter playerParameter;

        public virtual void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public virtual void Move(Vector3 moveVec)
        {
            if (rigidbody == null)
                return;

            if (playerAnimationPlayer.IsLanding)
                return;

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

