using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;
        [SerializeField] private float jumpPower = 3;

        public void StartJump()
        {
            playerAnimationPlayer.PlayJumpAnimation(this.GetCancellationTokenOnDestroy(), Jump).Forget();
        }

        private void Jump()
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        }
    }
}

