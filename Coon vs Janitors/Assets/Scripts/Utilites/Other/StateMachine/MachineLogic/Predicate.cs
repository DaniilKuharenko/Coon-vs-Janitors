using System;

namespace Raccons_House_Games
{
    public class Predicate : IPredicate
    {
        readonly Func<bool> func;

        public Predicate(Func<bool> func)
        {
            this.func = func;
        }

        public bool Evaluate() => func.Invoke();
    }
}
