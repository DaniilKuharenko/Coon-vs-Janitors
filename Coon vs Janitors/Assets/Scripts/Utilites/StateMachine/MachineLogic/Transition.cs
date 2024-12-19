using System;

namespace Raccons_House_Games
{
    public class Transition : ITransition
    {
        public IState To { get; private set; }
        public IPredicate Condition { get; private set; }
        public Action OnTransition { get; private set; }

        public Transition(IState to, IPredicate condition, Action onTransition = null)
        {
            To = to;
            Condition = condition;
            OnTransition = onTransition;
        }
    }
}
