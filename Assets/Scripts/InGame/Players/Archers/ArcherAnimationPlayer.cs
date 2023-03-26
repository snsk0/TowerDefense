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
        public bool IsAiming { get; private set; }

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

        public async UniTask PlayAttackAnimation(CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.NormalAttack);
            IsAttacking = true;
            IsAiming = true;
            StartAimWalkAnimation();
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Attack, AnimatorStateHashes.Attack, animator, token);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Attack, AnimatorStateHashes.Attack, animator, token, toState: false);
            IsAttacking = false;
            IsAiming = false;
            StopAimWalkAnimation();
        }
    }
}

