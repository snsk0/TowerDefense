using UnityEngine;

using Runtime.Enemy.Animation;
using Runtime.Enemy.Parameter;

namespace Runtime.Enemy.Component
{
    public class EnemyDamaged : MonoBehaviour
    {
        [SerializeField] protected EnemyAnimator animator;
        [SerializeField] protected new Rigidbody rigidbody;
        [SerializeField] protected EnemyParameter parameter;

        public bool isDamaging { get; private set; } = false;

        public virtual void KnockBack(Vector3 direction, float strong)
        {
            isDamaging = true;

            float knockBack = strong * parameter.poise;



            if (/*knockBack > 0.25 &&*/ knockBack < 0.5)
            {
                transform.forward = -direction;
                animator.PlayDamaged(0);
            }
            else if (knockBack >= 0.5)
            {
                transform.forward = -direction;
                animator.PlayDamaged(1);
                rigidbody.velocity = Vector3.zero;
            }
        }



        private float timer = 0;
        public void Update()
        {
            if (isDamaging)
            {
                timer += Time.deltaTime;
                if (timer > 0.7f) isDamaging = false;
            }


            else
            {
                timer = 0;
            }
        }


    }
}
