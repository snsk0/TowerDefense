using InGame.Damages;
using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace InGame.Enemies
{
    public class Enemy : MonoBehaviour, IEnemyDamagable
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private BoxCollider collider;

        public bool IsDamagable { get; private set; }

        private void Start()
        {
            collider.OnTriggerEnterAsObservable()
                .Select(other => other.GetComponent<IPlayerDamagable>())
                .Where(player => player != null)
                .Where(player=>player.IsDamagable)
                .Subscribe(player =>
                {
                    player.ApplyDamage(new Damage(10));
                    collider.enabled = false;
                })
                .AddTo(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Attack();
            }
        }

        public void ApplyDamage(Damage damage)
        {
            enemyHealth.AddDamage((int)damage.attackValue);
        }

        private void Attack()
        {
            collider.enabled = true;
        }
    }
}

