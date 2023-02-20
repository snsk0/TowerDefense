using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer.Unity;
using VContainer;

namespace InGame.Enhancements
{
    public class EnhancementPresenter : ControllerBase, IStartable
    {
        private readonly PlayerManager playerManager;
        private readonly EnhancementView enhancementView;

        [Inject]
        public EnhancementPresenter(PlayerManager playerManager, EnhancementView enhancementView)
        {
            this.playerManager = playerManager;
            this.enhancementView = enhancementView;
        }

        public void Start()
        {
            enhancementView.parameterUpButtonClickObservable
                .Subscribe(pair =>
                {
                    playerManager.playerParameter.IncreaseAddParameter(pair.Key, pair.Value);
                })
                .AddTo(this);
        }
    }
}

