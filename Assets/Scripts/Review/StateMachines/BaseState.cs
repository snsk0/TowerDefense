using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachine
{
    public class BaseState
    {
        private List<Transition> transitionList;
        public IEnumerable<Transition> Transitions => transitionList;
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

