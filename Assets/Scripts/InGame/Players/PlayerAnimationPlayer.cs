using Cysharp.Threading.Tasks;
using InGame.Damages;
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
        public bool IsLanding { get; private set; }

        public bool IsIdle => animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.Base).tagHash == AnimatorStateHashes.Idle;
        public bool IsAttackMotion => animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.Base).tagHash == AnimatorStateHashes.Attack;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public void PlayRunAnimation()
        {
            animator.SetBool(AnimatorTriggerHashes.Run, true);
        }

        public void StopRunAnimation()
        {
            animator.SetBool(AnimatorTriggerHashes.Run, false);
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
                await UniTask.DelayFrame(1, cancellationToken: token);
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
            //実際に浮き始めるまで待機
            await AnimationTransitionWaiter.WaitStateTime(0.25f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            jumpCallback?.Invoke();
            //着地まで待機
            await AnimationTransitionWaiter.WaitStateTime(0.57f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            IsLanding = true;
            //Idleモーションに遷移するまで待機
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Base, AnimatorStateHashes.Idle, animator, token);
            IsJumping = false;
            IsLanding = false;
        }

        public async UniTask PlayDamagedAnimation(KnockbackType knockbackType, CancellationToken token)
        {
            switch (knockbackType)
            {
                case KnockbackType.None:
                    Debug.Log("ノックバック無し！");
                    break;
                case KnockbackType.Huge:
                    animator.SetTrigger("HugeDamaged");
                    await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Base, AnimatorStateHashes.Damaged, animator, token);
                    while (true)
                    {
                        rigidbody.AddForce((-transform.forward+Vector3.up*0.25f) * 10);
                        if (animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.Base).normalizedTime>=0.45f)
                            break;

                        await UniTask.DelayFrame(1, cancellationToken: token);
                    }
                    break;
                default:
                    Debug.Log("ノックバック！");
                    break;
            }
        }
    }
}

