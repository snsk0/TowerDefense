using Cysharp.Threading.Tasks;
using InGame.Players.Animators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterAnimationPlayer : PlayerAnimationPlayer
    {
        public bool IsConnectableSecondAttack { get; private set; }
        public bool IsConnectableThirdAttack { get; private set; }

        public async UniTask PlayFirstAttackAnimation(CancellationToken token, Action<bool> attackCallback = null)
        {
            if (IsAttacking)
                return;

            if (IsJumping)
                return;

            animator.SetTrigger(AnimatorParameterHashes.FirstAttack);
            IsAttacking = true;
            //攻撃判定のタイミングまで待つ
            await AnimationTransitionWaiter.WaitStateTime(0.65f, (int)AnimatorLayerType.Base, AnimatorStateHashes.FirstAttack, animator, token, HashType.Name);
            //攻撃判定有効化のコールバック
            attackCallback?.Invoke(true);
            await AnimationTransitionWaiter.WaitStateTime(0.8f, (int)AnimatorLayerType.Base, AnimatorStateHashes.FirstAttack, animator, token, HashType.Name);
            attackCallback?.Invoke(false);
            IsAttacking = false;

            IsConnectableSecondAttack = true;
            await AnimationTransitionWaiter.WaitStateTime(1.03f, (int)AnimatorLayerType.Base, AnimatorStateHashes.FirstAttack, animator, token, HashType.Name);
            IsConnectableSecondAttack = false;
        }

        public async UniTask PlaySecondAttackAnimation(CancellationToken token, Action<bool> attackCallback = null)
        {
            IsConnectableSecondAttack = false;

            animator.SetTrigger(AnimatorParameterHashes.SecondAttack);
            IsAttacking = true;
            //攻撃判定のタイミングまで待つ
            await AnimationTransitionWaiter.WaitStateTime(0.36f, (int)AnimatorLayerType.Base, AnimatorStateHashes.SecondAttack, animator, token, HashType.Name);
            //攻撃判定有効化のコールバック
            attackCallback?.Invoke(true);
            await AnimationTransitionWaiter.WaitStateTime(0.72f, (int)AnimatorLayerType.Base, AnimatorStateHashes.SecondAttack, animator, token, HashType.Name);
            attackCallback?.Invoke(false);
            IsAttacking = false;

            IsConnectableThirdAttack = true;
            await AnimationTransitionWaiter.WaitStateTime(1.1f, (int)AnimatorLayerType.Base, AnimatorStateHashes.SecondAttack, animator, token, HashType.Name);
            IsConnectableThirdAttack = false;
        }

        public async UniTask PlayThirdAttackAnimation(CancellationToken token, Action<bool> attackCallback = null)
        {
            IsConnectableThirdAttack = false;

            animator.SetTrigger(AnimatorParameterHashes.ThirdAttack);
            IsAttacking = true;
            //攻撃判定のタイミングまで待つ
            await AnimationTransitionWaiter.WaitStateTime(0.33f, (int)AnimatorLayerType.Base, AnimatorStateHashes.ThirdAttack, animator, token, HashType.Name);
            //攻撃判定有効化のコールバック
            attackCallback?.Invoke(true);
            await AnimationTransitionWaiter.WaitStateTime(0.51f, (int)AnimatorLayerType.Base, AnimatorStateHashes.ThirdAttack, animator, token, HashType.Name);
            attackCallback?.Invoke(false);
            IsAttacking = false;
        }
    }
}

