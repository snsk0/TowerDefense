using UnityEngine;

using Data.Enemy;


namespace Runtime.Enemy.Parameter
{
    public class EnemyParameter : MonoBehaviour
    {
        //共通定数
        public static float sprintMultiply { get; } = 1.2f;


        //コンポーネント関連
        [SerializeField] private EnemyData data;


        //パラメータ群
        public int level { get; private set; }      //レベル(wave依存で初期化関数から)
        public float maxHealth                      //最大Hp
        {
            get { return data.maxHealth * (1 + (((float)level * data.growth) / 10)); }
        }
        public float attack                         //攻撃パラメータ
        {
            get { return data.attack * (1 + (((float)level * data.growth) / 10)); }
        }
        public float speed                          //攻撃速度
        {
            get { return data.speed; }
        }
        public float poise
        {
            get { return data.poise; }
        }
        public float exp                            //経験値
        {
            get { return data.exp * (1 + (((float)level * data.growth) / 10)); }
        }
        public float growth                         //成長補正
        {
            get { return data.growth; }
        }


        //初期化
        public void Initialize(int level)
        {
            this.level = level;
        }
    }
}
