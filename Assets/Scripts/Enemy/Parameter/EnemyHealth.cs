using UnityEngine;
using UniRx;


namespace Enemy.Parameter
{
    //最大HPだけScriptableObjectの意味があまりないがHealthは別コンポーネントで使うため目をつむる
    public class EnemyHealth : MonoBehaviour, IDamagable
    {
        //フィールド
        public float maxHealth { get; private set; }   //最大Hp
        private ReactiveProperty<float> _healthProperty;
        public IReadOnlyReactiveProperty<float> healthProperty => _healthProperty;
        public float currentHealth => healthProperty.Value;




        //初期化関数(外部からmaxHealthを設定できるように)
        public void Initialize(float maxHealth)
        {
            this.maxHealth = maxHealth;
            _healthProperty = new ReactiveProperty<float>(maxHealth);  //ReactiveProperty初期化
        }


        //ダメージ関数
        public void Damage(float damage, GameObject cause)
        {
            _healthProperty.Value -= damage;   //ダメージ
        }



        //無効化されたらReactivePropertyを無効化
        public void OnDisable()
        {
            _healthProperty.Dispose();
        }
    }
}
