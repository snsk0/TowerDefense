using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    public abstract class StateMachineControllerFactory
    {
        public abstract StateMachineController CreateStatemachineController();
    }
}

