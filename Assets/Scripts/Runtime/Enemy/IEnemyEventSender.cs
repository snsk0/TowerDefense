using System;
using UniRx;


namespace Runtime.Enemy
{
    public interface IEnemyEventSender
    {
        public IObservable<Unit> onMove { get; }
        public IObservable<Unit> onAttack { get; }
        public IObservable<EnemyDamageEvent> onDamage { get; }
    }


    public class EnemyDamageEvent
    {
        public float maxHealth { get; private set; }
        public float currentHealth { get; private set; }
        public float damage { get; private set; }


        public EnemyDamageEvent(float maxHealth, float currentHealth, float damage)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.damage = damage;
        }
    }
}