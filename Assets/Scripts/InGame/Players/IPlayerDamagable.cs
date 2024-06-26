using InGame.Damages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public interface IPlayerDamagable
    {
        public bool IsDamagable { get; }

        void ApplyDamage(Damage damage);
    }
}

