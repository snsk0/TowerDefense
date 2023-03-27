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

        public async UniTask NormalAttack(IEnemyDamagable target, CancellationToken token)
        {
            if (archerAnimationPlayer.currentAttackState!=PlayerAttackStateType.None)
                return;

            if (target == null)
                return;

            await archerAnimationPlayer.PlayNormalAttackAnimation(this.GetCancellationTokenOnDestroy());

            var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue);
            var damage = new Damage(attackValue, KnockbackType.None);
            target.ApplyDamage(damage);
        }

        public async UniTask SpecialAttack(IEnemyDamagable target, CancellationToken token)
        {
            if (archerAnimationPlayer.currentAttackState != PlayerAttackStateType.None)
                return;

            if (target == null)
                return;

            if (!usableSpecial)
                return;

            StartCoolTimeCount(this.GetCancellationTokenOnDestroy()).Forget();
            await archerAnimationPlayer.PlaySpecialAttackAnimation(this.GetCancellationTokenOnDestroy());

            var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue) * 6;
            var damage = new Damage(attackValue, KnockbackType.None);
            target.ApplyDamage(damage);
        }
    }
}

