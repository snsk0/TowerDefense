using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Review.StateMachines
{
    public abstract class StateMachineControllerFactory
    {
        protected StateMachineFactory stateMachineFactory { get; private set; }

        [Inject]
        protected StateMachineControllerFactory(StateMachineFactory stateMachineFactory)
        {
            this.stateMachineFactory = stateMachineFactory;
        }

        protected abstract StateMachineController CreateStateMachineController(Type controllerType, GameObject targetObject);
    }
}

