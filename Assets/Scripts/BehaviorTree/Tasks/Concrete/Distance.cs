using UnityEngine;

namespace BehaviorTree.Tasks.Concrete
{
    public class Distance : ActionTask
    {
        [SerializeField] private Transform target;
        [SerializeField] private float distance;

        public override TaskStatus OnUpdate()
        {
           if(Vector3.Distance(target.position, owner.transform.position) < distance) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}