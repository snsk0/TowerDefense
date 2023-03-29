using UnityEngine;

using Runtime.Enemy.Animation;


namespace Runtime.Enemy.Component
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected EnemyAnimator animator;


        public bool isAttacking { get; protected set; }

        public abstract void Attack(int index, GameObject target);
    }
}
