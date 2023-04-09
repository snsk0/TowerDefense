using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Review.StateMachines
{
    [CreateAssetMenu(menuName ="StateMachineSetting", fileName ="StateMachineSetting")]
    public class StateMachineSetting : ScriptableObject
    {
        [SerializeField] private List<StateMachineSetting> subStateMachineSettings;
        [SerializeField] private List<BaseStateObject> stateObjects;
        [SerializeField] private BlackboardSetting blackboardSetting;
        [SerializeField] private List<Transition> transitions;

        //private IEnumerable<Transition> oldTransitions;

        public IEnumerable<StateMachineSetting> SubStateMachineSettings => subStateMachineSettings;
        public IEnumerable<BaseStateObject> StateObjects => stateObjects;
        //public KeyValuePair<string, BaseStateObject[]> subStates(StateMachineSetting stateMachineSetting)
        //    => new KeyValuePair<string, BaseStateObject[]>(stateMachineSetting.name, stateMachineSetting.states.ToArray()); 
        
        private void OnValidate()
        {
            if (blackboardSetting == null)
                return;

            //if (oldTransitions == null)
            //    return;

            //var newTransitions = transitions.Except(oldTransitions);
            foreach (var condition in transitions.SelectMany(x => x.Conditions))
            {
                condition.SetBlackboardSetting(blackboardSetting);
            }

            //foreach(var transition in transitions)
            //{
            //    transition.SetStateObjects(states, subStateMachineSettings.Select(x=>subStates(x)));
            //}
            //oldTransitions = transitions;

            //subStateMachineTypes = subStateMachines.Select(x => Type.GetType(Enum.GetName(typeof(StateMachineType), x)));
            //stateTypes = states.Select(x => Type.GetType(Enum.GetName(typeof(StateType), x)));
            //stateTypes=states.Select(x=>x as BaseState)

            //if (stateTypes.Any(x => x == null))
            //{
            //    Debug.LogWarning("対応するステートクラスが存在しません");
            //}
        }
    }
}

