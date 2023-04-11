using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    public abstract class BaseStateObject : ScriptableObject
    {
        public abstract string stateName { get; protected set; }
        public abstract BaseState CreateState();
    }
}

