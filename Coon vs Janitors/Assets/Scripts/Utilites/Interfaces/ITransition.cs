using System;

namespace Raccons_House_Games
{
    public interface ITransition
    {
        IState To {get;}
        IPredicate Condition { get; }
        Action OnTransition { get; }
    }
}
