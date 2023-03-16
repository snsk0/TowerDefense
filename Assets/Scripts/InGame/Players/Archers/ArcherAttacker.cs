using Cysharp.Threading.Tasks;
using InGame.Damages;
using InGame.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace InGame.Players.Archers
{
    public class ArcherAttacker : PlayerAttacker
    {
        [SerializeField] private ArcherAnimationPlayer archerAnimationPlayer;

        //private int chargeLevel;
        //private IEnemyDamagable targetEnemy;
        //private TargetSearcher targetSearcher;

        //private CancellationTokenSource tokenSource;

        //public void SetTargetSearcher(TargetSearcher targetSearcher)
        //{
        //    this.targetSearcher = targetSearcher;
        //}

        public void Attack(IEnemyDamagable target)
        {
            //var target = targetSearcher.SerchTarget(transform.position);
            if (target == null)
                return;

            archerAnimationPlayer.PlayAttackAnimation(this.GetCancellationTokenOnDestroy()).Forget();

            var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification;
            var damage = new Damage(attackValue, KnockbackType.None);
            target.ApplyDamage(damage);
        }

        //public void StartCharge()
        //{
        //    archerAnimationPlayer.PlayChargeAnimation();
        //    tokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), new CancellationToken());
        //    Charge(tokenSource.Token).Forget();
        //}

        //private async UniTask Charge(CancellationToken token)
        //{
        //    chargeLevel = 0;
        //    await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
        //    chargeLevel = 1;
        //    await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
        //    chargeLevel = 2;
        //}

        //public void Release()
        //{
        //    archerAnimationPlayer.PlayReleaseAnimation();

        //    //チャージ量によってダメージ倍率を変化させる
        //    var magnification = chargeLevel switch
        //    {
        //        0 => 0.9f,
        //        1 => 1.1f,
        //        2 => 1.2f,
        //        _=>0,
        //    };

        //    var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification * magnification;
        //    var target = targetEnemy == null ? targetSearcher.SerchTarget(transform.position) : targetEnemy;
        //    var damage = new Damage(attackValue, KnockbackType.None);
        //    target.ApplyDamage(damage);

        //    tokenSource.Cancel();
        //    tokenSource.Dispose();
        //}

        //public void RockOn()
        //{
        //    var target = targetSearcher.SerchTarget(transform.position);
        //    targetEnemy = target;
        //}
    }
}

