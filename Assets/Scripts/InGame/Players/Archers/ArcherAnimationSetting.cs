using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using InGame.Players.Animators;
using VContainer.Unity;
using VContainer;
using UnityEditor.Animations;

namespace InGame.Players.Archers
{
    public class ArcherAnimationSetting : ControllerBase, IInitializable
    {
        private readonly PlayerManager playerManager;

        private Animator animator;

        [Inject]
        public ArcherAnimationSetting(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public void Initialize()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    animator = player.GetComponent<Animator>();
                    UpdateAttackAnimationSpeed();
                })
                .AddTo(this);
        }

        private void UpdateAttackAnimationSpeed()
        {
            //アニメーターにあるアニメーションを取得
            var animatorController = animator.runtimeAnimatorController as AnimatorController;
            var layer = animatorController.layers[(int)AnimatorLayerType.NormalAttack];
            var states = layer.stateMachine.states.Select(x => x.state);
            //アニメーション自体の長さを取得
            var drawAnimationLength = (states.Single(x => x.name == "Standing Draw Arrow").motion as AnimationClip).length;
            var recoilAtackAnimationLength = (states.Single(x => x.name == "Standing Aim Recoil").motion as AnimationClip).length;
            var normalAttackAnimationLength = drawAnimationLength + recoilAtackAnimationLength;

            //アニメーションの長さがインターバルと同じ長さになるように調整
            var normalAttackSpped = normalAttackAnimationLength / playerManager.playerParameter.AttackInterval;
            animator.SetFloat(AnimatorParameterHashes.NormalAttackSpeed, normalAttackSpped);

            playerManager.playerParameter.ObserveEveryValueChanged(x => x.AttackInterval)
                .Subscribe(interval =>
                {
                    var normalAttackSpped = normalAttackAnimationLength / playerManager.playerParameter.AttackInterval;
                    animator.SetFloat(AnimatorParameterHashes.NormalAttackSpeed, normalAttackSpped);
                })
                .AddTo(this);

        }
    }
}

