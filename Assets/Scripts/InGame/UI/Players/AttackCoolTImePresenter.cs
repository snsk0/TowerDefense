using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.UI.Players
{
    public class AttackCoolTimePresenter : ControllerBase, IInitializable
    {
        private readonly PlayerManager playerManager;
        private readonly AttackCoolTimeView attackCoolTimeView;

        private PlayerAttacker playerAttacker;

        [Inject]
        public AttackCoolTimePresenter(PlayerManager playerManager, AttackCoolTimeView attackCoolTimeView)
        {
            this.playerManager = playerManager;
            this.attackCoolTimeView = attackCoolTimeView;
        }

        public void Initialize()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    playerAttacker = player.GetComponent<PlayerAttacker>();
                    StartViewCoolTime();
                })
                .AddTo(this);
        }

        private void StartViewCoolTime()
        {
            playerAttacker.RemainingTimeObservable
                .Subscribe(time =>
                {
                    var count = Mathf.CeilToInt(time);
                    var rate = 1 - (time / playerManager.playerParameter.GetCalculatedValue(PlayerParameterType.SpecialAttackCoolTime));
                    attackCoolTimeView.ViewSpecialAttackCoolTime(count, rate);
                })
                .AddTo(this);
        }
    }
}

