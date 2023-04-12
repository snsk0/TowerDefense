using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using System;
using VContainer;

namespace Review.StateMachines
{
    public class StateMachineManager : ControllerBase
    {
        private readonly StateMachineFactory stateMachineFactory;

        private List<StateMachine> usingStateMachines = new List<StateMachine>();
        public IEnumerable<StateMachine> UsingStateMachines => usingStateMachines;

        [Inject]
        public StateMachineManager(StateMachineFactory stateMachineFactory)
        {
            this.stateMachineFactory = stateMachineFactory;
            ObserveCreatedStateMachine();
        }

        private void ObserveCreatedStateMachine()
        {
            stateMachineFactory.CreatedStateMachineObservable
                .Subscribe(SM =>
                {
                    RegisterStateMachine(SM);
                })
                .AddTo(this);
        }

        private void RegisterStateMachine(StateMachine stateMachine)
        {
            usingStateMachines.Add(stateMachine);
        }
    }
}

