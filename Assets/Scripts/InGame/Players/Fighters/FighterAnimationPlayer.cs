using Cysharp.Threading.Tasks;
using InGame.Players.Animators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterAnimationPlayer : PlayerAnimationPlayer
    {
        [SerializeField] private AvatarMask upperBodyMask; 

        public void PlayNormalAttackAniamtion(Action<bool> setEnableAttackCollider)
        {
            var token = this.GetCancellationTokenOnDestroy();
            var stateInfo = animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.NormalAttack);
            if (stateInfo.tagHash == AnimatorParameterHashes.Attack)
            {
                if (stateInfo.shortNameHash == AnimatorStateHashes.FirstNormalAttack)
                {
                    PlayNormalAttackAnimation(setEnableAttackCollider, 0.5f, token).Forget();
                }
                else if(stateInfo.shortNameHash == AnimatorStateHashes.SecondNormalAttack)
                {
                    PlayNormalAttackAnimation(setEnableAttackCollider, 0.78f, token).Forget();
                }
                else
                {
                    Debug.LogWarning("アニメーションは攻撃状態ではありません");
                }
            }
            else
            {
                PlayNormalAttackAnimation(setEnableAttackCollider, 0.78f, token).Forget();
            }
        }

        private async UniTaskVoid PlayNormalAttackAnimation(Action<bool> setEnableAttackCollider, float attackTiming, CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.NormalAttack);
            //var token = this.GetCancellationTokenOnDestroy();
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.Normal;
            await AnimationTransitionWaiter.WaitStateTime(attackTiming, (int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            setEnableAttackCollider(true);
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.None;
            setEnableAttackCollider(false);
        }

        public async UniTask PlaySpecialAttackAnimation(Action<bool> setEnableAttackCollider, CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.SpecialAttack);
            //var token = this.GetCancellationTokenOnDestroy();
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.Special;
            await AnimationTransitionWaiter.WaitStateTime(0.33f, (int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            setEnableAttackCollider(true);
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.None;
            setEnableAttackCollider(false);
        }
    }
}

