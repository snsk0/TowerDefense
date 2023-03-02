using UnityEngine;

namespace Runtime.Enemy
{
    public interface IEnemyDamagable
    {
        public void Damage(float damage, float hate, GameObject cause)
        {
            Damage(damage, 0, hate, cause);
        }
        public void Damage(float damage, float knock, float hate, GameObject cause);
    }
}
