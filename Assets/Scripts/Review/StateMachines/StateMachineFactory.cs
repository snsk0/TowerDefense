using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Review.StateMachine
{
    public class StateMachineFactory
    {
        private readonly ISubject<StateMachine> createdStateMachineSubject = new Subject<StateMachine>();
        public IObservable<StateMachine> CreatedStateMachineobservable => createdStateMachineSubject;

        public StateMachine CreateStateMachine(StateMachineSetting stateMachineSetting)
        {
            var stateMachine = new StateMachine(stateMachineSetting);
            return stateMachine;
            //var stateMachine = Activator.CreateInstance(stateMachineType);
            //if(stateMachine is StateMachine)
            //{
            //    createdStateMachineSubject.OnNext(stateMachine as StateMachine);
            //    return stateMachine as StateMachine;
            //}
            //else
            //{
            //    Debug.LogError($"{stateMachineType}がステートマシンではありません");
            //    return null;
            //}
        }
    }
}

