using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Linq;
using InGame.Players.Animators;
using UnityEditor.Animations;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.AddressableAssets;
using System;

namespace InGame.Players.Fighters
{
    public class FighterAnimationSetting : ControllerBase, IInitializable, IDisposable
    {
        private readonly PlayerManager playerManager;

        private Animator animator;
        private FighterAnimationPlayer fighterAnimationPlayer;

        private CancellationTokenSource tokenSource;

        [Inject]
        public FighterAnimationSetting(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public void Initialize()
        {
            tokenSource = new CancellationTokenSource();

            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    animator = player.GetComponent<Animator>();
                    fighterAnimationPlayer = player.GetComponent<FighterAnimationPlayer>();
                    UpdateAttackAnimationSpeed();
                    //AdjustAvaterMask(tokenSource.Token).Forget();
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
            var firstNormalAttackAnimationLength = (states.Single(x => x.name == "FirstNormalAttack").motion as AnimationClip).length;
            var SecondNormalAtackAnimationLength = (states.Single(x => x.name == "SecondNormalAttack").motion as AnimationClip).length;

            //アニメーションの長さがインターバルと同じ長さになるように調整
            var firstNormalAttackSpped = firstNormalAttackAnimationLength / playerManager.playerParameter.NormalAttackInterval;
            var secondNormalAttackSpped = SecondNormalAtackAnimationLength / playerManager.playerParameter.NormalAttackInterval;
            animator.SetFloat(AnimatorParameterHashes.FirstNormalAttackSpeed, firstNormalAttackSpped);
            animator.SetFloat(AnimatorParameterHashes.SecondNormalAttackSpeed, secondNormalAttackSpped);

            playerManager.playerParameter.ObserveEveryValueChanged(x => x.NormalAttackInterval)
                .Subscribe(interval =>
                {
                    var firstNormalAttackSpped = firstNormalAttackAnimationLength / playerManager.playerParameter.NormalAttackInterval;
                    var secondNormalAttackSpped = SecondNormalAtackAnimationLength / playerManager.playerParameter.NormalAttackInterval;
                    animator.SetFloat(AnimatorParameterHashes.FirstNormalAttackSpeed, firstNormalAttackSpped);
                    animator.SetFloat(AnimatorParameterHashes.SecondNormalAttackSpeed, secondNormalAttackSpped);
                })
                .AddTo(this);

        }

        public async UniTask AdjustAvaterMask(CancellationToken token)
        {
            var animatorController = animator.runtimeAnimatorController as AnimatorController;
            var layer = animatorController.layers[(int)AnimatorLayerType.NormalAttack];
            var uppderBodyMask = await Addressables.LoadAssetAsync<AvatarMask>("UpperBodyMask");
            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                await UniTask.WaitUntil(() => fighterAnimationPlayer.currentBaseState == PlayerBaseStateType.Move, cancellationToken: token);

                layer.avatarMask = uppderBodyMask;
                (animator.runtimeAnimatorController as AnimatorController).layers[(int)AnimatorLayerType.NormalAttack] = layer;
                //animator.runtimeAnimatorController = animatorController;

                Debug.Log((animator.runtimeAnimatorController as AnimatorController).layers[(int)AnimatorLayerType.NormalAttack].avatarMask);

                await UniTask.WaitUntil(() => fighterAnimationPlayer.currentBaseState != PlayerBaseStateType.Move, cancellationToken: token);
                layer.avatarMask = null;
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}

