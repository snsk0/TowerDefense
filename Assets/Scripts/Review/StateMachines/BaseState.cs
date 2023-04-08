using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachine
{
    [Serializable]
    public class BaseState : UnityEngine.Object
    {
        private Blackboard blackboard;

        public void Execute()
        {

        }

        public void Abort()
        {

        }

        private void FinishState()
        {

        }
    }
}

