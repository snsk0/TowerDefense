using System;
using UnityEngine;

namespace Review.StateMachines.States.StateObjects{
   public class IdleStateObject : BaseStateObject
   {
        public override string stateName { get;} = "IdleState";
        public override Type stateType { get; } = typeof(IdleState);
        public override BaseState CreateState()
        {
            return new IdleState();
        }
    }
}
