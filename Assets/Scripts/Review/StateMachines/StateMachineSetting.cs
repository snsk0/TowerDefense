using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachine
{
    [CreateAssetMenu(menuName ="StateMachineSetting")]
    public class StateMachineSetting : ScriptableObject
    {
        public Script stateType;
    }
}

