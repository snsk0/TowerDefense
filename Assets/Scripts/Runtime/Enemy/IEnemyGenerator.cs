using UnityEngine;


namespace Runtime.Enemy
{
    public interface IEnemyGenerator
    {
        public EnemyType enemyType { get; }
        public EnemyController Generate(Transform transform);
    }
}