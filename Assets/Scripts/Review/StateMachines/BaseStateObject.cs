using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    public abstract class BaseStateObject : ScriptableObject
    {
        public abstract string stateName { get; }
        public abstract Type stateType { get; }
        public abstract BaseState CreateState();
    }
}

