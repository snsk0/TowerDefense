using UnityEngine;
using UnityEngine.AI;

using Enemy;

namespace BehaviorTree.Tasks.Concrete
{
    public class NavMeshSeek : ActionTask
    {
        private NavMeshAgent agent;
        [SerializeField] private Transform target;
        [SerializeField] private float endDistance;

        [SerializeField] private EnemyController controller;

        //èâä˙âª
        public override void OnAwake()
        {
            agent = owner.GetComponent<NavMeshAgent>();
        }


        public override void OnStart()
        {
            agent.isStopped = false;
        }

        //í«ê’
        public override TaskStatus OnUpdate()
        {
            /*
            agent.SetDestination(target.position);

            if (agent.pathPending | agent.remainingDistance > endDistance) return TaskStatus.Running;
            else
            {
                agent.isStopped = true;
                return TaskStatus.Success;
            }*/

            controller.MoveToTarget();
            return TaskStatus.Running;
        }

    }
}
