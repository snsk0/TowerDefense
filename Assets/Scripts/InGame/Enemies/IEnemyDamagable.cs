using InGame.Damages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Enemies
{
    public interface IEnemyDamagable
    {
        bool IsDamagable { get; }

        void ApplyDamage(Damage damage);
    }
}

