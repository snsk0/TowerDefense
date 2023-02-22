using InGame.Damages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Enemies
{
    public class Enemy : MonoBehaviour, IEnemyDamagable
    {
        [SerializeField] private EnemyHealth enemyHealth;

        public bool IsDamagable { get; private set; }

        public void ApplyDamage(Damage damage)
        {
            enemyHealth.AddDamage((int)damage.attackValue);
        }
    }
}

