using UnityEngine;

using Runtime.Enemy;
using Runtime.Enemy.Component;

namespace Runtime.Wave
{
    public class Tower : MonoBehaviour, IDamagable
    {
        [SerializeField] private EnemyHealth health;


        private void OnEnable()
        {
            health.Initialize();
        }


        public void Damage(float damage, Vector3 knock, float hate, GameObject cause)
        {
            health.SetDamage(damage);
        }
    }
}
