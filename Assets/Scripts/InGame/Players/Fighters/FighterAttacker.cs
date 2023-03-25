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
        [SerializeField] private FighterAnimationPlayer fighterAnimationPlayer;
        [SerializeField] private FighterAttackCollider fighterNormalAttackCollider;
        [SerializeField] private FighterAttackCollider fighterSpecialAttackCollider;

        private void Start()
        {
            fighterNormalAttackCollider.OnTriggerEnterAsObservable()
                .Select(trigger => trigger.GetComponent<IEnemyDamagable>())
                .Where(enemy => enemy != null)
                .Subscribe(enemy =>
                {
                    var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification;
                    var damage = new Damage(attackValue, KnockbackType.None);
                    enemy.ApplyDamage(damage);
                })
                .AddTo(this);


            fighterSpecialAttackCollider.OnTriggerEnterAsObservable()
                .Select(trigger => trigger.GetComponent<IEnemyDamagable>())
                .Where(enemy => enemy != null)
                .Subscribe(enemy =>
                {
                    var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification * 3f;
                    var damage = new Damage(attackValue, KnockbackType.None);
                    enemy.ApplyDamage(damage);
                })
                .AddTo(this);
        }

        public void NormalAttack()
        {
            fighterAnimationPlayer.PlayNormalAttackAniamtion(fighterNormalAttackCollider.EnableCollider);
        }

        public async UniTask SpecialAttack()
        {
            if (fighterAnimationPlayer.currentAttackState!=PlayerAttackStateType.None)
                return;

            if (!usableSpecial)
                return;

            StartCoolTimeCount(this.GetCancellationTokenOnDestroy()).Forget();
            await fighterAnimationPlayer.PlaySpecialAttackAnimation(fighterSpecialAttackCollider.EnableCollider, this.GetCancellationTokenOnDestroy());
        }
    }
}