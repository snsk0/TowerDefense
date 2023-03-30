using UnityEngine;

using UniRx;

using Runtime.Enemy.Parameter;



namespace Runtime.Enemy.Component
{
    public class EnemyHealth : MonoBehaviour
    {
        //コンポーネント
        [SerializeField] private EnemyParameter parameter;


        //フィールド
        public float maxHealth => parameter.maxHealth;
        private ReactiveProperty<float> _currentHealth = new ReactiveProperty<float>();
        public IReactiveProperty<float> currentHealth => _currentHealth;



        //初期化
        public void Initialize()
        {
            _currentHealth.Value = maxHealth;
        }


        //ダメージ
        public void SetDamage(float damage)
        {
            //Hp計算
            float health = _currentHealth.Value - damage;
            if (health < 0) health = 0;

            //値の更新
            _currentHealth.Value = health;
        }


        //Dispose
        private void OnDestroy()
        {
            _currentHealth.Dispose();
        }
    }
}
