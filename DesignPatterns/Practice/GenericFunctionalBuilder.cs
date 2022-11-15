using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice
{
    public abstract class GenericFunctionalBuilder<TSubject, TSelf>
            where TSubject : new()
            where TSelf : GenericFunctionalBuilder<TSubject, TSelf>
    {
        /// <summary>
        ///  To hold a list fluent actions.
        /// </summary>
        private readonly List<Func<TSubject, TSubject>> actions = new List<Func<TSubject, TSubject>>();

        /// <summary>
        /// Helps in adding functions to a list of action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public TSelf Do(Action<TSubject> action)
        {
            AddActions(action);
            return (TSelf)this;
        }

        /// <summary>
        /// Adds all fuctions to a list
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private TSelf AddActions(Action<TSubject> action)
        {
            actions.Add(p => {
                action(p);
                return p;
            });
            return (TSelf)this;
        }

        /// <summary>
        /// Builds Subject In Very Fluent Way
        /// </summary>
        /// <returns></returns>
        public TSubject Build()
        {
            return actions.Aggregate(new TSubject(), (p, f) => f(p));
        }
    }
}
