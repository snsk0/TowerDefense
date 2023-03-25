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

        public async UniTaskVoid Attack(IEnemyDamagable target)
        {
            if (archerAnimationPlayer.currentAttackState!=PlayerAttackStateType.None)
                return;

            if (target == null)
                return;

            await archerAnimationPlayer.PlayAttackAnimation(this.GetCancellationTokenOnDestroy());

            Debug.Log("Attack");
            var attackValue = (playerParameter.baseAttackValue + playerParameter.addAttackValue) * playerParameter.attackMagnification;
            var damage = new Damage(attackValue, KnockbackType.None);
            target.ApplyDamage(damage);
        }
    }
}

