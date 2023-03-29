using UnityEngine;

namespace BehaviorTree.Tasks.Concrete
{
    public class LogTask : ActionTask
    {
        [SerializeField] private string log;

        public override TaskStatus OnUpdate()
        {
            Debug.Log(log);
            return TaskStatus.Success;
        }
    }
}
