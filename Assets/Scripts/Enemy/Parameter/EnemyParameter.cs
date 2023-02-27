using UnityEngine;



namespace Enemy.Parameter
{
    public class EnemyParameter : MonoBehaviour
    {
        //コンポーネント関連
        [SerializeField] private EnemyData data;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyHate _hate;

        //アクセッサ
        public EnemyHealth health => _health;
        public EnemyHate hate => _hate;


        //パラメータ群
        public int level { get; private set; }      //レベル(wave依存で初期化関数から)
        public float maxHealth                      //最大Hp
        {
            get { return data.maxHealth * (((float)level * data.growth) / 10); }
        }
        public float attack                         //攻撃パラメータ
        {
            get { return data.attack * (((float)level * data.growth) / 10); }
        }
        public float speed                          //攻撃速度
        {
            get { return data.speed * (((float)level * data.growth) / 100); }
        }
        public float growth                         //成長補正
        {
            get { return data.growth; }
        }



        //パラメータの初期化処理を行う
        private void Awake()
        {
            health.Initialize(maxHealth);
        }
    }
}
