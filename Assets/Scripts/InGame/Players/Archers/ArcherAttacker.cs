using Cysharp.Threading.Tasks;
using InGame.Damages;
using InGame.Enemies;
using Runtime.Enemy;
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

        public async UniTask NormalAttack(IDamagable target)
        {
            if (archerAnimationPlayer.currentAttackState!=PlayerAttackStateType.None)
                return;

            if (target == null)
                return;

            await archerAnimationPlayer.PlayNormalAttackAnimation(this.GetCancellationTokenOnDestroy());

            var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue);
            //var damage = new Damage(attackValue, KnockbackType.None);
            target.Damage(attackValue, 1, gameObject);
        }

        public async UniTask SpecialAttack(IDamagable target)
        {
            if (archerAnimationPlayer.currentAttackState != PlayerAttackStateType.None)
                return;

            if (target == null)
                return;

            if (!usableSpecial)
                return;

            
            await archerAnimationPlayer.PlaySpecialAttackAnimation(this.GetCancellationTokenOnDestroy());

            var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue) * 6;
            StartCoolTimeCount(this.GetCancellationTokenOnDestroy()).Forget();
            //var damage = new Damage(attackValue, KnockbackType.None);
            target.Damage(attackValue, 1, gameObject);
        }
    }
}

