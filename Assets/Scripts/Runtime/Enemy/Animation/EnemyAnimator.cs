using UnityEngine;


namespace Runtime.Enemy.Animation
{
    public class EnemyAnimator : MonoBehaviour
    {
        //アニメータ
        [SerializeField] protected Animator animator;


        //立ちモーション
        public virtual void PlayIdle()
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }


        //歩き
        public virtual void PlayWalk(Vector3 vector, float maxVelocity)
        {
            vector = vector / maxVelocity;

            animator.SetBool("Walk", true);
            animator.SetFloat("Walk_X", vector.x);
            animator.SetFloat("Walk_Z", vector.z);
        }

        
        //走り
        public virtual void PlayRun(Vector3 vector, float maxVelocity)
        {
            vector = vector / maxVelocity;

            animator.SetBool("Run", true);
            animator.SetFloat("Run_X", vector.x);
            animator.SetFloat("Run_Z", vector.z);
        }


        //攻撃
        public virtual void PlayAttack(int index)
        {
            animator.SetTrigger("Attack_" + index);
        }
        

        //ノックバック
        public virtual void PlayDamaged(int index)
        {
            animator.SetTrigger("Damaged_" + index);
        }


        //死亡
        public virtual void PlayDeath()
        {
            animator.SetTrigger("Death");
        }


        //その他アニメーション
        public virtual void PlayUnique(int index)
        {
            animator.SetTrigger("Unique_" + index);
        }
    }
}
