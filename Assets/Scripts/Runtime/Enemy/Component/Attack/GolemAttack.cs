using UnityEngine;



namespace Runtime.Enemy.Component.Attack
{
    public class GolemAttack : EnemyAttack
    {
        private float timer;

        public override void Attack(int index, GameObject target)
        {
            animator.PlayAttack(index);
            timer = 0;
        }


        public void Update()
        {
            if (isAttacking)
            {
                timer += Time.deltaTime;
                if (timer > 0.7f)
                {
                    isAttacking = false;
                }
            }

            else
            {
                timer = 0;
            }
        }
    }
}
