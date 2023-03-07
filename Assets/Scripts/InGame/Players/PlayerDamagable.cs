using InGame.Damages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerDamagable : MonoBehaviour, IPlayerDamagable
    {
        [SerializeField] private PlayerHealth playerHealth;

        public bool IsDamagable { get; private set; } = true;

        public void SetDamagable(bool value)
        {
            IsDamagable = value;
        }

        public void ApplyDamage(Damage damage)
        {
            playerHealth.AddDamage((int)damage.attackValue);
        }
    }
}

