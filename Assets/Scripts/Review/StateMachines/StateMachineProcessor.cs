using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Review.StateMachines
{
    public class StateMachineProcessor : IInitializable, IDisposable
    {
        private readonly StateMachineManager stateMachineManager;
        private readonly CancellationTokenSource tokenSource;

        [Inject]
        public StateMachineProcessor(StateMachineManager stateMachineManager)
        {
            this.stateMachineManager = stateMachineManager;
            tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            StartProcessStateMachine(tokenSource.Token).Forget();
        }

        private async UniTask StartProcessStateMachine(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                foreach (var SM in stateMachineManager.UsingStateMachines)
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

