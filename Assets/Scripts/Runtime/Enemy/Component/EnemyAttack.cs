using UnityEngine;


namespace Runtime.Enemy.Component
{
    public abstract class EnemyAttack : MonoBehaviour
    {



        //攻撃用メソッド
        public abstract float AttackToTarget(GameObject target, int index);


        //キャンセル用
        public virtual void CancellAtack()
        {

        }
    }
}
