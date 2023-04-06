using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Review.StateMachine
{
    public class StateMachineManager
    {
        private List<StateMachine> usingStateMachines;

        private void RegisterStateMachine(StateMachine stateMachine)
        {
            usingStateMachines.Add(stateMachine);
        }

        private async void StartProcessStateMachine(CancellationToken token)
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
    }
}

