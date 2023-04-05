using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Review.StateMachine
{
    public class Transition
    {
        public BaseState beforeState { get; }
        public BaseState afterState { get; }
        public Func<bool> canTransition { get; }
    }
}

