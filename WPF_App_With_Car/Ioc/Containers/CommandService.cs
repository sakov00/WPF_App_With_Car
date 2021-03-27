using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_App_With_Car.Ioc.Containers
{
    public static class CommandService
    {
        private static readonly ConcurrentDictionary<Type, ICommand> instances = new ConcurrentDictionary<Type, ICommand>();

        public static void Register<T>() where T : ICommand
        => instances[typeof(T)] = null;

        public static T Resolve<T>() where T : ICommand
            => GetCommand<T>(obj=> { }, null);

        public static T GetCommand<T>(Action<object> execute, Func<object, bool> canExecute) where T : ICommand
        {
            object[] args = new object[2];
            args[0]= execute;
            args[1] = canExecute;
               // typeof(T).GetConstructors().First().GetParameters().ToArray();
            if (instances.TryGetValue(typeof(T), out ICommand command))
            {
                if (instances[typeof(T)] == null)
                {
                    command = (T)Activator.CreateInstance(typeof(T), args);
                    instances[typeof(T)] = command;
                }
                else
                {
                    command = instances[typeof(T)];
                }
            }
            return (T)command;

        }
    }
}
