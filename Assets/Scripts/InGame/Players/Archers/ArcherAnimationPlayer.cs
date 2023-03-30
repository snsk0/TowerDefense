using Cysharp.Threading.Tasks;
using InGame.Players.Animators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players.Archers
{
    public class ArcherAnimationPlayer : PlayerAnimationPlayer
    {
        private ArcherEffectPlayer archerEffectPlayer;

        public bool IsAiming { get; private set; }

        private void Start()
        {
            archerEffectPlayer = playerEffectPlayer as ArcherEffectPlayer;
        }

        public void StartAimWalkAnimation()
        {
            animator.SetBool(AnimatorParameterHashes.AimWalking, true);
        }

        public void StopAimWalkAnimation()
        {
            animator.SetBool(AnimatorParameterHashes.AimWalking, false);
        }

        public void PlayAimWalkAnimation(float xValue, float yValue)
        {
            animator.SetFloat(AnimatorParameterHashes.AimMoveX, xValue);
            animator.SetFloat(AnimatorParameterHashes.AimMoveY, yValue);
        }

        public async UniTask PlayNormalAttackAnimation(CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.NormalAttack);
            archerEffectPlayer.PlayNormalAttackEffect(token).Forget();
            currentAttackState = PlayerAttackStateType.Normal;
            IsAiming = true;
            StartAimWalkAnimation();
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.NormalAttack, AnimatorStateHashes.Attack, animator, token, toState: false);
            currentAttackState = PlayerAttackStateType.None;
            IsAiming = false;
            StopAimWalkAnimation();
            
        }

        public async UniTask PlaySpecialAttackAnimation(CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.SpecialAttack);
            currentAttackState = PlayerAttackStateType.Special;
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.SpecialAttack, AnimatorStateHashes.Attack, animator, token, toState: false);
            currentAttackState = PlayerAttackStateType.None;
        }
    }
}

