using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_App_With_Car.Ioc.Interfaces;

namespace WPF_App_With_Car.Ioc.Containers
{
    public static class ViewModelService
    {
        private static readonly ConcurrentDictionary<Type, IViewModel> instances = new ConcurrentDictionary<Type, IViewModel>();
                public static void Register<T>() where T : IViewModel
        => instances[typeof(T)] = null;

        public static T Resolve<T>() where T : IViewModel
            => GetViewModel<T>();
        public static T GetViewModel<T>() where T : IViewModel
        {
            if (instances.TryGetValue(typeof(T), out IViewModel viewModel))
            {
                if (instances[typeof(T)] == null)
                {
                    viewModel = (T)Activator.CreateInstance(typeof(T));
                    instances[typeof(T)] = viewModel;
                }
                else
                {
                    viewModel = instances[typeof(T)];
                }
            }
            return (T)viewModel;
        }
    }
}
