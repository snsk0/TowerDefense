using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.Players
{
    public class PlayerGeneratePresenter : ControllerBase, IStartable
    {
        private readonly PlayerManager playerManager;
        private readonly PlayerController playerController;

        [Inject]
        public PlayerGeneratePresenter(PlayerManager playerManager, PlayerController playerController)
        {
            this.playerManager = playerManager;
            this.playerController = playerController;
        }

        public void Start()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    playerController.StartControll(player);
                })
                .AddTo(this);

            playerManager.GeneratePlayer(PlayerCharacterType.Fighter);
        }
    }
}

