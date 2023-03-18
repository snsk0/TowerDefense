using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Linq;
using InGame.Players.Animators;
using UnityEditor.Animations;

namespace InGame.Players.Fighters
{
    public class FighterAnimationSetting : ControllerBase, IInitializable
    {
        private readonly PlayerManager playerManager;

        private Animator animator;

        [Inject]
        public FighterAnimationSetting(PlayerManager playerManager)
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
            var layer = animatorController.layers[(int)AnimatorLayerType.Attack];
            var states = layer.stateMachine.states.Select(x => x.state);
            //アニメーション自体の長さを取得
            var firstNormalAttackAnimationLength = (states.Single(x => x.name == "FirstNormalAttack").motion as AnimationClip).length;
            var SecondNormalAtackAnimationLength = (states.Single(x => x.name == "SecondNormalAttack").motion as AnimationClip).length;

            //アニメーションの長さがインターバルと同じ長さになるように調整
            var firstNormalAttackSpped = firstNormalAttackAnimationLength / playerManager.playerParameter.AttackInterval;
            var secondNormalAttackSpped = SecondNormalAtackAnimationLength / playerManager.playerParameter.AttackInterval;
            animator.SetFloat(AnimatorParameterHashes.FirstNormalAttackSpeed, firstNormalAttackSpped);
            animator.SetFloat(AnimatorParameterHashes.SecondNormalAttackSpeed, secondNormalAttackSpped);

            playerManager.playerParameter.ObserveEveryValueChanged(x => x.AttackInterval)
                .Subscribe(interval =>
                {
                    var firstNormalAttackSpped = firstNormalAttackAnimationLength / playerManager.playerParameter.AttackInterval;
                    var secondNormalAttackSpped = SecondNormalAtackAnimationLength / playerManager.playerParameter.AttackInterval;
                    animator.SetFloat(AnimatorParameterHashes.FirstNormalAttackSpeed, firstNormalAttackSpped);
                    animator.SetFloat(AnimatorParameterHashes.SecondNormalAttackSpeed, secondNormalAttackSpped);
                })
                .AddTo(this);

        }
    }
}

