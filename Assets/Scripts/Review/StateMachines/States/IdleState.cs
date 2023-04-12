using UnityEngine;

namespace Review.StateMachines.States{
   public class IdleState : BaseState
   {
        public IdleState()
        {
            IsRepeatable = true;
        }

        public override void Execute(Blackboard blackboard)
        {
            Debug.Log("Idle");
            result = ResultType.Success;
        }

        public override void Abort()
        {
            base.Abort();
        }

        protected override void FinishState()
        {
            base.FinishState();
        }
    }
}
