using System;
using System.Collections.Generic;

namespace Raccons_House_Games
{
    public class StateMachine
    {
        protected IState currentState;
        public IState CurrentState => currentState;
        //Transition rules
        private Dictionary<IState, List<ITransition>> stateTransitions = new Dictionary<IState, List<ITransition>>();
        private HashSet<ITransition> anyTransitions = new HashSet<ITransition>(); // Global transitions (any state)


        public void SetState (IState newState)
        {
            currentState?.OnExit();
            currentState = newState;
            currentState?.OnEnter();
        }

        public void AddTransition (IState from, IState to, IPredicate condition, Action onTransition = null)
        {
            if(!stateTransitions.ContainsKey(from))
            {
                stateTransitions[from] = new List<ITransition>();
            }
            stateTransitions[from].Add(new Transition(to, condition, onTransition));
        }

        public void AddAnyTransition (IState to, IPredicate condition, Action onTransition = null)
        {
            anyTransitions.Add(new Transition(to, condition, onTransition));
        }

        //Check for valid Transitions
        private ITransition GetTransition()
        {
            // First, check global transitions
            foreach(var transition in anyTransitions)
            {
                if(transition.Condition.Evaluate())
                    return transition;
            }

            // Then, check transitions for the current state
            if(stateTransitions.TryGetValue(currentState, out var possibleTransitions))
            {
                foreach(var transition in possibleTransitions)
                {
                    if(transition.Condition.Evaluate())
                        return transition;
                }
            }

            return null;
        }

        public void Update()
        {
            //Check if transition can occur
            var transition = GetTransition();
            if(transition != null)
            {
                transition.OnTransition?.Invoke();
                SetState(transition.To);
            }

            currentState?.Update();
        }

    }
}
