using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Review.StateMachines
{
    public class StateMachine
    {
        private StateMachine parent;

        private IEnumerable<StateMachine> subStateMachines;
        private IEnumerable<Transition> transitions;
        private IEnumerable<BaseState> states;
        
        private BaseState currentState;
        private BaseState entryState;
        private Blackboard blackboard;

        public bool IsProsessing { get; protected set; }

        public StateMachine(IEnumerable<StateMachine> subStateMachines, IEnumerable<Transition> transitions,
            IEnumerable<BaseState> states, Blackboard blackboard) 
        {
            SetSubStateMachines(subStateMachines);
            SetTransitions(transitions);
            SetStates(states);
            SetBlackboard(blackboard);

            entryState = states.ElementAt(0);
            Init();
        }

        private void SetSubStateMachines(IEnumerable<StateMachine> subStateMachines)
        {
            this.subStateMachines = subStateMachines;
        }

        private void SetTransitions(IEnumerable<Transition> transitions)
        {
            this.transitions = transitions;
        }

        private void SetStates(IEnumerable<BaseState> states)
        {
            this.states = states;
        }

        private void SetBlackboard(Blackboard blackboard)
        {
            this.blackboard = blackboard;
        }

        

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

        public void Restart()
        {
            currentState = entryState;
            IsProsessing = true;
        }

        public void Tick()
        {
            Debug.Log("Tick");
            //currentState.Execute();
            //CheckTransition();
        }

        private void CheckTransition()
        {
            //foreach(var transition in transitionList.Where(x=>x.canTransition()))
            //{
            //    ChangeState(transition.afterState);
            //}

            //foreach (var transition in currentState.Transitions.Where(x => x.canTransition()))
            //{
            //    ChangeState(transition.afterState);
            //}
        }

        private void ChangeState(BaseState nextState)
        {
            currentState.Abort();

            //if (states.Any(x => x == nextState.GetType()))
            //{
            //    currentState = nextState;
            //}
            //else
            //{
            //    currentState = null;
            //    IsProsessing = false;
            //    //var subStateMachine = subStateMachines.Single(x => x.states.Any(y => y == nextState.GetType()));
            //    //subStateMachine.Init();
            //}
        }

        private void ExitState()
        {
            if (parent!=null)
            {
                parent.Restart();
            }
            else
            {
                currentState = entryState;
            }
        }
    }
}