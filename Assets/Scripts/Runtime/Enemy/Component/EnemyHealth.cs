using UnityEngine;

using Runtime.Enemy.Parameter;


namespace Runtime.Enemy.Component
{
    public class EnemyHealth : MonoBehaviour
    {
        //パラメータ
        [SerializeField] private EnemyParameter parameter;


        public float maxHealth => parameter.maxHealth;      //最大Hp参照
        public float currentHealth { get; private set; }    //現在Hp



        //Hp計算
        public virtual float Damage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            else if (currentHealth < 0) currentHealth = 0;

            return damage;
        }




        //Hpの初期化
        private void OnEnable()
        {
            currentHealth = maxHealth;
        }
    }
}
