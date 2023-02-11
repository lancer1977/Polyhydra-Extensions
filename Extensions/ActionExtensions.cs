using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyhydraGames.Extensions
{
    public static class ActionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="act"></param>
        /// <param name="wrappedVariable"></param>
        /// <returns></returns>
        public static Action<Action> ToContinueAction<T>(this Action<T, Action> act, T wrappedVariable)
        {
            return a => act(wrappedVariable, a);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <param name="followup"></param>
        /// <returns></returns>
        public static Action BuildAction(Action<Action> option, Action followup)
        {
            return () => option(followup);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        public static Action AggregateActionList(this List<Action<Action>> actions)
        {
            return actions.Aggregate<Action<Action>, Action>(null, (current, act) => BuildAction(act, current));
        }

        /// <summary>
        /// Executes an action times = x
        /// </summary>
        /// <param name="act"></param>
        /// <param name="times"></param>
        public static void Execute(this Action act, int times = 1)
        {
            if (times <= 0) return;
            for (var x = 0; x < times; x++)
            {
                act();
            }

        }
        
        public static async Task ExecuteAsync(this Func<Task> act, int times = 1)
        {
            if (times <= 0)
                return;
            for (var index = 0; index < times; ++index)
                await act();
        }
    }
}