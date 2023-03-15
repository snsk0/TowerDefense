using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;
using Prepare;

namespace InGame.Players
{
    public class PlayerGeneratePresenter : ControllerBase, IStartable
    {
        private readonly PlayerManager playerManager;
        private readonly PlayerController playerController;
        private readonly PrepareSetting prepareSetting;

        [Inject]
        public PlayerGeneratePresenter(PlayerManager playerManager, PlayerController playerController, PrepareSetting prepareSetting)
        {
            this.playerManager = playerManager;
            this.playerController = playerController;
            this.prepareSetting = prepareSetting;
        }

        public void Start()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    playerController.StartControll(player);
                })
                .AddTo(this);

            playerManager.GeneratePlayer(prepareSetting.selectedPlayerCharacterType);
        }
    }
}

