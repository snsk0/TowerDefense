using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using InGame.Damages;
using InGame.Enemies;

namespace InGame.Players.Fighters
{
    public class FighterAttacker : PlayerAttacker
    {
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;
        [SerializeField] private FighterAttackCollider fighterAttackCollider;

        private void Start()
        {
            fighterAttackCollider.OnTriggerEnterAsObservable()
                .Select(trigger => trigger.GetComponent<IEnemyDamagable>())
                .Where(enemy => enemy != null)
                .Subscribe(enemy =>
                {
                    var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification;
                    var damage = new Damage(attackValue);
                    enemy.ApplyDamage(damage);
                })
                .AddTo(this);
        }

        public override void Attack()
        {
            playerAnimationPlayer.PlayAttackAnimation(this.GetCancellationTokenOnDestroy(), fighterAttackCollider.EnableCollider).Forget();
        }
    }
}