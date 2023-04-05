using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachine
{
    public class StateMachineController
    {
        private StateMachine useStateMachine;

        private StateMachineFactory stateMachineFactory;

        public StateMachineController(Type useStateMachineClass)
        {
            useStateMachine = stateMachineFactory.CreateStateMachine(useStateMachineClass);
        }
    }
}

