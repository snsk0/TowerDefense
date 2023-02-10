using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InGame.Players
{
    public class PlayerGeneratePresenter : IStartable
    {
        private PlayerManager playerManager;

        [Inject]
        public PlayerGeneratePresenter(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public void Start()
        {
            playerManager.GeneratePlayer(PlayerCharacterType.Fighter);
        }
    }
}

