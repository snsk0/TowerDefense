using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using System;

namespace Review.StateMachines
{
    public class StateMachineManager : ControllerBase, IDisposable
    {
        private StateMachineFactory stateMachineFactory;

        private List<StateMachine> usingStateMachines;
        private CancellationTokenSource tokenSource;

        public StateMachineManager()
        {
            ObserveCreatedStateMachine();
            tokenSource = new CancellationTokenSource();
            StartProcessStateMachines(tokenSource.Token);
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

        private async void StartProcessStateMachines(CancellationToken token)
        {
            while (true)
            {
                foreach(var SM in usingStateMachines)
                {
                    SM.Tick();
                }
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}

