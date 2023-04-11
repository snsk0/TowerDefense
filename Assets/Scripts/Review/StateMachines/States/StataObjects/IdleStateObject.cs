using UnityEngine;

namespace Review.StateMachines.States.StateObjects{
   public class IdleStateObject : BaseStateObject
   {
        public override string stateName { get; protected set; } = "IdleState";
        public override BaseState CreateState()
        {
            return new IdleState();
        }
    }
}
