using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using InGame.Players.Animators;
using VContainer.Unity;
using VContainer;

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
            var animatorController = animator.runtimeAnimatorController;
            var drawAnimation = animatorController.animationClips.Single(x => x.name == "Standing Draw Arrow");
            var recoilAnimation = animatorController.animationClips.Single(x => x.name == "Standing Aim Recoil");
            var normalAttackLength = drawAnimation.length + recoilAnimation.length;

            var spped = normalAttackLength / playerManager.playerParameter.AttackInterval;
            animator.SetFloat(AnimatorParameterHashes.NormalAttackInterval, spped);

            playerManager.playerParameter.ObserveEveryValueChanged(x => x.AttackInterval)
                .Subscribe(interval =>
                {
                    var spped = normalAttackLength / interval;
                    animator.SetFloat(AnimatorParameterHashes.NormalAttackInterval, spped);
                })
                .AddTo(this);

        }
    }
}

