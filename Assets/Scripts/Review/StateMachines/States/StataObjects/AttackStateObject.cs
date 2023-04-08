using UnityEngine;

namespace Review.StateMachines.States.StateObjects
{
    [CreateAssetMenu(menuName ="StateObject/AttackState", fileName =("AttackStateObject"))]
    public class AttackStateObject : BaseStateObject
    {
        public override string stateName { get; protected set; } = "AttackState";
        public override BaseState state { get; protected set; } = new AttackState();
    }
}

