using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Review.StateMachine
{
    public abstract class StateMachine
    {
        private List<Transition> transitionList;
        private StateMachine parent;
        private List<StateMachine> subStateMachines;
        private List<Type> states;
        private BaseState currentState;
        private BaseState entryState;
        private Blackboard blackboard;

        public bool IsProsessing { get; protected set; }

        protected abstract void SetStates();
        protected abstract void SetTransitions();

        public void Init()
        {
            currentState = entryState;
            IsProsessing = true;
        }

        public void Init(BaseState activeState)
        {
            Init();
            currentState = activeState;
        }

        public void Tick()
        {
            currentState.Execute();
            CheckTransition();
        }

        private void CheckTransition()
        {
            foreach(var transition in transitionList.Where(x=>x.canTransition()))
            {
                ChangeState(transition.afterState);
            }

            foreach (var transition in currentState.Transitions.Where(x => x.canTransition()))
            {
                ChangeState(transition.afterState);
            }
        }

        private void ChangeState(BaseState nextState)
        {
            currentState.Abort();

            if (states.Any(x => x == nextState.GetType()))
            {
                currentState = nextState;
            }
            else
            {
                currentState = null;
                IsProsessing = false;
                var subStateMachine = subStateMachines.Single(x => x.states.Any(y => y == nextState.GetType()));
                subStateMachine.Init();
            }
        }

        private void ExitState()
        {
            if (parent!=null)
            {
                parent.Init();
            }
            else
            {
                currentState = entryState;
            }
        }
    }
}