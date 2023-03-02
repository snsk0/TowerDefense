using UnityEngine;

using Runtime.Enemy;



namespace BehaviorTree.Tasks.Concrete
{
    public class AttackTask : ActionTask
    {
        [SerializeField] private EnemyController controller;
        private float attackTime;
        private float deltTime;


        public override void OnStart()
        {
            attackTime = controller.Attack(0);
            deltTime = 0;
        }


        public override TaskStatus OnUpdate()
        {
            deltTime += Time.deltaTime;

            if(deltTime > attackTime)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
    }
}
