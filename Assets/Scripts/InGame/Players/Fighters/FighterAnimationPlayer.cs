using Cysharp.Threading.Tasks;
using InGame.Players.Animators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterAnimationPlayer : PlayerAnimationPlayer
    {
        public void PlayNormalAttackAniamtion(Action<bool> setEnableAttackCollider)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.Attack);
            if (stateInfo.tagHash == AnimatorParameterHashes.Attack)
            {
                if (stateInfo.shortNameHash == AnimatorStateHashes.FirstNormalAttack)
                {
                    PlayNormalAttackAnimation(setEnableAttackCollider, 0.5f).Forget();
                }
                else if(stateInfo.shortNameHash == AnimatorStateHashes.SecondNormalAttack)
                {
                    PlayNormalAttackAnimation(setEnableAttackCollider, 0.78f).Forget();
                }
                else
                {
                    Debug.LogWarning("アニメーションは攻撃状態ではありません");
                }
            }
            else
            {
                PlayNormalAttackAnimation(setEnableAttackCollider, 0.78f).Forget();
            }
        }

        private async UniTaskVoid PlayNormalAttackAnimation(Action<bool> setEnableAttackCollider, float attackTiming)
        {
            animator.SetTrigger(AnimatorParameterHashes.NormalAttack);
            var token = this.GetCancellationTokenOnDestroy();
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Attack, AnimatorStateHashes.Attack, animator, token);
            IsAttacking = true;
            await AnimationTransitionWaiter.WaitStateTime(attackTiming, (int)AnimatorLayerType.Attack, AnimatorStateHashes.Attack, animator, token);
            setEnableAttackCollider(true);
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.Attack, AnimatorStateHashes.Attack, animator, token);
            IsAttacking = false;
            setEnableAttackCollider(false);
        }
    }
}

