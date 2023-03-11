using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;

        private PlayerParameter playerParameter;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public void Move(Vector3 moveVec)
        {
            if (rigidbody == null)
                return;

            var moveSpeed = (playerParameter.baseMoveSpeed + playerParameter.addMoveSpeed) * playerParameter.moveSpeedMagnification;
            var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.velocity = velocity;
        }
    }
}

