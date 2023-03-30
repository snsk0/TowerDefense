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
        private FighterEffectPlayer fighterEffectPlayer;

        private void Start()
        {
            fighterEffectPlayer = playerEffectPlayer as FighterEffectPlayer;
        }

        public void PlayNormalAttackAniamtion(Action<bool> setEnableAttackCollider)
        {
            var token = this.GetCancellationTokenOnDestroy();
            var stateInfo = animator.GetCurrentAnimatorStateInfo((int)AnimatorLayerType.NormalAttack);
            if (stateInfo.tagHash == AnimatorParameterHashes.Attack)
            {
                if (stateInfo.shortNameHash == AnimatorStateHashes.FirstNormalAttack)
                {
                    Action playEffectAction = () => fighterEffectPlayer.PlayNormalAttackEffect(true);
                    PlayNormalAttackAnimation(setEnableAttackCollider, playEffectAction, 0.5f, token).Forget();
                }
                else if(stateInfo.shortNameHash == AnimatorStateHashes.SecondNormalAttack)
                {
                    Action playEffectAction = () => fighterEffectPlayer.PlayNormalAttackEffect(false);
                    PlayNormalAttackAnimation(setEnableAttackCollider, playEffectAction, 0.78f, token).Forget();
                }
                else
                {
                    Debug.LogWarning("アニメーションは攻撃状態ではありません");
                }
            }
            else
            {
                Action playEffectAction = () => fighterEffectPlayer.PlayNormalAttackEffect(true);
                PlayNormalAttackAnimation(setEnableAttackCollider, playEffectAction, 0.78f, token).Forget();
            }
        }

        private async UniTaskVoid PlayNormalAttackAnimation(Action<bool> setEnableAttackCollider, Action playEffectAction, float attackTiming, CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.NormalAttack);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.Normal;
            await AnimationTransitionWaiter.WaitStateTime(attackTiming, (int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            playEffectAction?.Invoke();
            setEnableAttackCollider(true);
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.None;
            setEnableAttackCollider(false);
        }

        public async UniTask PlaySpecialAttackAnimation(Action<bool> setEnableAttackCollider, CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.SpecialAttack);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.Special;
            await AnimationTransitionWaiter.WaitStateTime(0.33f, (int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            fighterEffectPlayer.playSpecialAttackEffect();
            setEnableAttackCollider(true);
            await AnimationTransitionWaiter.WaitStateTime(1f, (int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            currentAttackState = PlayerAttackStateType.None;
            setEnableAttackCollider(false);
        }
    }
}

