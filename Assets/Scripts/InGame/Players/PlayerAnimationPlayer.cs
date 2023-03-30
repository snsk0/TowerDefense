using Cysharp.Threading.Tasks;
using InGame.Damages;
using InGame.Players.Animators;
using InGame.Players.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace InGame.Players
{
    public class PlayerAnimationPlayer : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody rigidbody;

        private PlayerInput playerInput = new PlayerInput();
        private PlayerParameter playerParameter;

        public PlayerBaseStateType currentBaseState { get; protected set; }
        public PlayerAttackStateType currentAttackState { get; protected set; }

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public void PlayRunAnimation()
        {
            currentBaseState = PlayerBaseStateType.Move;
            animator.SetBool(AnimatorParameterHashes.Run, true);
        }

        public void StopRunAnimation()
        {
            currentBaseState = PlayerBaseStateType.Idle;
            animator.SetBool(AnimatorParameterHashes.Run, false);
        }

        public void StartAvoidAnimation()
        {
            currentBaseState = PlayerBaseStateType.Sprint;
            animator.SetBool(AnimatorParameterHashes.Sprint, true);
        }

        public void EndAvoidAnimation()
        {
            currentBaseState = PlayerBaseStateType.Idle;
            animator.SetBool(AnimatorParameterHashes.Sprint, false);
        }

        public async UniTask PlayJumpAnimation(CancellationToken token, Action jumpCallback = null)
        {
            currentBaseState = PlayerBaseStateType.Jump;

            animator.SetTrigger(AnimatorParameterHashes.Jump);
            //実際に浮き始めるまで待機
            await AnimationTransitionWaiter.WaitStateTime(0.25f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            jumpCallback?.Invoke();
            //着地まで待機
            await AnimationTransitionWaiter.WaitStateTime(0.57f, (int)AnimatorLayerType.Base, AnimatorStateHashes.Jump, animator, token);
            //Idleモーションに遷移するまで待機
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Base, AnimatorStateHashes.Idle, animator, token);

            currentBaseState = PlayerBaseStateType.Idle;
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

                    this.FixedUpdateAsObservable()
                        .TakeWhile(_ => animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.Base).normalizedTime <= 0.4f)
                        .Subscribe(_ =>
                        {
                            rigidbody.AddForce((-transform.forward + Vector3.up * 0.375f) * 30);
                        })
                        .AddTo(this);
                    break;
                default:
                    Debug.Log("ノックバック！");
                    break;
            }
        }

        public void PlayDeathAnimation()
        {
            animator.SetBool(AnimatorParameterHashes.Death, true);
        }
    }
}

