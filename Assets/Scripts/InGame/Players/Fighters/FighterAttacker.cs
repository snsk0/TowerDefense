using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace InGame.Players.Fighters
{
    public class FighterAttacker : PlayerAttacker
    {
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;
        [SerializeField] private FighterAttackCollider fighterAttackCollider;

        private void Start()
        {
            fighterAttackCollider.OnTriggerEnterAsObservable()
                .Subscribe(_ =>
                {
                    var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification;
                })
                .AddTo(this);
        }

        public override void Attack()
        {
            playerAnimationPlayer.PlayAttackAnimation(this.GetCancellationTokenOnDestroy(), fighterAttackCollider.EnableCollider).Forget();
        }
    }
}