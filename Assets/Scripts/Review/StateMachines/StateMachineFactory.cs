using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Review.StateMachines
{
    public class StateMachineFactory
    {
        private readonly ISubject<StateMachine> createdStateMachineSubject = new Subject<StateMachine>();
        public IObservable<StateMachine> CreatedStateMachineObservable => createdStateMachineSubject;

        public StateMachine CreateStateMachine(StateMachineSetting stateMachineSetting)
        {
            var subStateMachines = stateMachineSetting.SubStateMachineSettings.Select(x => CreateStateMachine(x));
            var transitions = stateMachineSetting.Transitions;
            var states = stateMachineSetting.StateObjects.Select(x => x.CreateState());
            var blackboard = CreateBlackBoard(stateMachineSetting.UseBlackboardSetting);

            var stateMachine= new StateMachine(subStateMachines, transitions, states, blackboard);

            createdStateMachineSubject.OnNext(stateMachine);

            return stateMachine;
        }

        private Blackboard CreateBlackBoard(BlackboardSetting blackboardSetting)
        {
            return new Blackboard(blackboardSetting.BlackboardKeyValuePairs.Select(x => new KeyValuePair<string, object>(x.KeyString, GetDeafultValue(x.ValueType))));
        }

        private object GetDeafultValue(BlackboardValueType valueType)
            => valueType switch
            {
                BlackboardValueType.Boolean => false,
                BlackboardValueType.Integer => 0,
                BlackboardValueType.Float => 0f,
                BlackboardValueType.MonoBehaviour => null,
                BlackboardValueType.GameObject => null,
                BlackboardValueType.Transform => null,
            };
    }
}

