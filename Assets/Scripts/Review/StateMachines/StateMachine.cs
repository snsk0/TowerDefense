using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Review.StateMachines
{
    public class StateMachine : IDisposable
    {
        private StateMachine parent;

        private IEnumerable<StateMachine> subStateMachines;
        private IEnumerable<Transition> transitions;
        private IEnumerable<BaseState> states;
        
        private BaseState currentState;
        private BaseState entryState;
        private Blackboard blackboard;

        private List<IDisposable> disposables = new List<IDisposable>();

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
            ExecuteState();
            IsProsessing = true;
        }

        public void Init(BaseState activeState)
        {
            Init();
            currentState = activeState;
            ExecuteState();
        }

        public void Restart()
        {
            Init();
        }

        public void Tick()
        {
            CheckStateResult();

            //CheckTransition();
        }

        private void ExecuteState()
        {
            currentState.Execute(blackboard);
        }

        private void CheckStateResult()
        {
            var result = currentState.result;
            switch (result)
            {
                case ResultType.Success:
                    if (currentState.IsRepeatable)
                    {
                        ExecuteState();
                    }
                    else
                    {
                        ChangeNextState();
                    }
                    break;
                case ResultType.False:
                    break;
                case ResultType.Processing:
                    return;
                case ResultType.Avort:
                    break;
            }
        }

        private void ChangeNextState()
        {
            var stateTransitions = transitions.Where(x => x.BeforeState == currentState);
            foreach(var transition in stateTransitions)
            {
                if (transition.Conditions.All(x => x.Condition()))
                {
                    currentState = transition.AfterState;
                    ExecuteState();
                    return;
                }
            }

            Debug.Log("ëJà⁄êÊÇ™ë∂ç›ÇµÇ‹ÇπÇÒ");
        }

        public void Dispose()
        {
            foreach(var disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}