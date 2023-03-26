using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.UI.Players
{
    public class PlayerHPPresenter : ControllerBase, IInitializable
    {
        private readonly PlayerManager playerManager;
        private readonly PlayerHPView playerHPView;

        private PlayerParameter playerParameter;
        private PlayerHealth playerHealth;

        [Inject]
        public PlayerHPPresenter(PlayerManager playerManager, PlayerHPView playerHPView)
        {
            this.playerManager = playerManager;
            this.playerHPView = playerHPView;
        }

        public void Initialize()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    playerParameter = playerManager.playerParameter;
                    playerHealth = playerManager.currentPlayerObject.GetComponent<PlayerHealth>();
                    UpdatePlayerHPUI();
                })
                .AddTo(this);
        }

        private void UpdatePlayerHPUI()
        {
            playerHealth.ObserveEveryValueChanged(x => x.currentHP)
                .Subscribe(hp =>
                {
                    var rate = (float)hp / playerParameter.MaxHP;
                    playerHPView.ViewHPBar(rate);
                    playerHPView.ViewHPValue(hp, playerParameter.MaxHP);
                })
                .AddTo(this);
        }
    }
}

