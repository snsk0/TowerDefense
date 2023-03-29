using UnityEngine;

using Runtime.Enemy;
using Runtime.Enemy.Component;
using InGame.Damages;
using InGame.Players;

namespace Runtime.Wave
{
    public class Tower : MonoBehaviour, IPlayerDamagable
    {
        [SerializeField] private EnemyHealth health;

        public bool IsDamagable { get; private set; }

        private void OnEnable()
        {
            health.Initialize();
        }

        public void ApplyDamage(Damage damage)
        {
            health.SetDamage(damage.attackValue);
        }


        //public void Damage(float damage, Vector3 knock, float hate, GameObject cause)
        //{
        //    health.SetDamage(damage);
        //}
    }
}
