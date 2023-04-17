using System;
using UnityEngine;

namespace Review.StateMachines.States.StateObjects
{
    [CreateAssetMenu(menuName ="StateObject/AttackState", fileName =("AttackStateObject"))]
    public class AttackStateObject : BaseStateObject
    {
        public override string stateName { get; } = "AttackState";
        public override Type stateType { get; } = typeof(AttackState);
        public override BaseState CreateState()
        {
            return new AttackState();
        }
    }
}

