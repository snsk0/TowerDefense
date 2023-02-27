using Cysharp.Threading.Tasks;
using InGame.Players.Animators;
using InGame.Players.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAnimationPlayer : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody rigidbody;

        private PlayerInput playerInput = new PlayerInput();
        private PlayerParameter playerParameter;

        public bool IsAttacking { get; protected set; }
        public bool IsJumping { get; private set; }

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public async UniTask PlayAvoidAnimationAsync(CancellationToken token)
        {
            //TODO:アニメーションの実行に書き換える
            var time = 0f;

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                var avoidDistance = (playerParameter.baseAvoidDistance + playerParameter.addAvoidDistance) * playerParameter.avoidDistanceMagnification;
                rigidbody.AddForce(playerInput.MoveVec * avoidDistance);
                await UniTask.DelayFrame(1);
                time += Time.deltaTime;
                var invisicibleTime = (playerParameter.baseInvincibleTime + playerParameter.addinvincibleTime) * playerParameter.invincibleTimeMagnification;
                if (time > invisicibleTime)
                    break;
            }
        }

        public async virtual UniTask PlayAttackAnimation(CancellationToken token, Action<bool> attackCallback = null)
        {
            //それぞれのキャラの子クラスで実装
        }

        public async UniTask PlayJumpAnimation(CancellationToken token, Action jumpCallback = null)
        {
            if (IsAttacking)
                return;

            if (IsJumping)
                return;

            animator.SetTrigger(AnimatorTriggerHashes.Jump);
            IsJumping = true;
            await AnimationTransitionWaiter.WaitStateTime(0.25f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            jumpCallback?.Invoke();
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            IsJumping = false;
        }
    }
}

