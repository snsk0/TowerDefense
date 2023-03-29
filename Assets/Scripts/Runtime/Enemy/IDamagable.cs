using UnityEngine;


namespace Runtime.Enemy
{
    public interface IDamagable
    {
        public void Damage(float damage, float hate, GameObject cause)
        {
            Damage(damage, Vector3.zero, hate, cause);
        }
        public void Damage(float damage, Vector3 knock, float hate, GameObject cause);
    }
}
