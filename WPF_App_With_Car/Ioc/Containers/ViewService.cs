using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_App_With_Car.Ioc.Interfaces;

namespace WPF_App_With_Car.Ioc.Containers
{
    public static class ViewService
    {
        private static readonly ConcurrentDictionary<Type, IView> instances = new ConcurrentDictionary<Type, IView>();

        public static void Register<T>() where T : IView
        => instances[typeof(T)] = null;

        public static T Resolve<T>(IViewModel viewModel) where T : IView
            => CreateView<T>(viewModel);
        public static T Resolve<T>() where T : IView
            => GetView<T>();

        public static T CreateView<T>(IViewModel viewModel) where T : IView
        {
            if (instances.TryGetValue(typeof(T), out IView view))
            {
                view = (T)Activator.CreateInstance(typeof(T));
                instances[typeof(T)] = view;
            }
            if (viewModel != null)
                view.DataContext = viewModel;
            return (T)view;
        }
        public static T GetView<T>() where T : IView
        {
            if (instances.TryGetValue(typeof(T), out IView view))
            {
                view=instances[typeof(T)];
            }
            return (T)view;
        }
    }
}
