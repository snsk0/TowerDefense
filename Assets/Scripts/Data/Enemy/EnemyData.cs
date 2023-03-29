using UnityEngine;


namespace Data.Enemy
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string _enemyName;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _attack;
        [SerializeField] private float _speed;
        [SerializeField] private float _poise;
        [SerializeField] private float _exp;
        [SerializeField] private float _growth;

        public string enemyName => _enemyName;
        public float maxHealth => _maxHealth;
        public float attack => _attack;
        public float speed => _speed;
        public float poise => _poise;
        public float exp => _exp;
        public float growth => _growth;
    }
}
