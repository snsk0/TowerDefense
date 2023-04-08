using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Review.StateMachine
{
    public class StateMachineController
    {
        protected StateMachineType stateMachineType;

        private StateMachineSetting stateMachineSetting;
        private StateMachine useStateMachine;
        private StateMachineFactory stateMachineFactory;

        public StateMachineController()
        {
            var path = $"StateMachine/{Enum.GetName(typeof(StateMachineType), stateMachineType)}";
            Addressables.LoadAssetAsync<StateMachineSetting>(path).Completed += setting =>
            {
                stateMachineFactory.CreateStateMachine(setting.Result);
            };
            
        }
    }
}

