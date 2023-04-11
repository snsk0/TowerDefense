using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Review.StateMachines
{
    [CreateAssetMenu(menuName = "StateMachineSetting", fileName = "StateMachineSetting")]
    public class StateMachineSetting : ScriptableObject
    {
        [SerializeField] private List<StateMachineSetting> subStateMachineSettings;
        [SerializeField] private List<BaseStateObject> stateObjects;
        [SerializeField] private BlackboardSetting blackboardSetting;
        [SerializeField] private List<Transition> transitions;

        public IEnumerable<StateMachineSetting> SubStateMachineSettings => subStateMachineSettings;
        public IEnumerable<BaseStateObject> StateObjects => stateObjects;
        public BlackboardSetting UseBlackboardSetting => blackboardSetting;
        public IEnumerable<Transition> Transitions => transitions;
    }
}

