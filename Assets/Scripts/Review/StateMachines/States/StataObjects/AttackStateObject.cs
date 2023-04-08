using UnityEngine;

namespace Review.StateMachine.States
{
    [CreateAssetMenu(menuName ="StateObject/AttackState", fileName =("AttackStateObject"))]
    public class AttackStateObject : BaseStateObject
    {
        public override string stateName { get; protected set; } = "AttackState";
        public override BaseState state { get; protected set; } = new AttackState();
    }
}

