using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    public static class StateFactory
    {
        public static BaseState CreateState(Type stateType)
        {
            var state = Activator.CreateInstance(stateType);
            if (state is BaseState)
            {
                //createdStateMachineSubject.OnNext(stateMachine as StateMachine);
                return state as BaseState;
            }
            else
            {
                Debug.LogError($"{stateType}がステートではありません");
                return null;
            }
        }
    }
}

