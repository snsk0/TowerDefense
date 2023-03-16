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
        public async UniTask PlayAttackAnimation(CancellationToken token)
        {
            animator.SetTrigger(AnimatorParameterHashes.Attack);
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Base, AnimatorStateHashes.Attack, animator, token);
            IsAttacking = true;
            await AnimationTransitionWaiter.WaitAnimationTransition((int)AnimatorLayerType.Base, AnimatorStateHashes.Idle, animator, token);
            IsAttacking = false;
        }
    }
}

