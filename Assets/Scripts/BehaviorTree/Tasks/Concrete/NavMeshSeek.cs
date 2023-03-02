using UnityEngine;

using Runtime.Enemy;


namespace BehaviorTree.Tasks.Concrete
{
    public class NavMeshSeek : ActionTask
    {
        [SerializeField] private EnemyController controller;


        //í«ê’
        public override TaskStatus OnUpdate()
        {
            bool isMoved = controller.MoveToTarget();

            if (isMoved) return TaskStatus.Running;
            else return TaskStatus.Success;
        }

    }
}
