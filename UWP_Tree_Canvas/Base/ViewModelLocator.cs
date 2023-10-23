using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UWP_Tree_Canvas.ViewModels;

namespace UWP_Tree_Canvas.Base
{
    internal class ViewModelLocator
    {
        private readonly IUnityContainer _container;

        public ViewModelLocator() 
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();
            _container.RegisterType<TreesUwpViewModel>();
            _container.RegisterType<CanvasUwpViewModel>();
        }

        public MainViewModel MainViewModel => _container.Resolve<MainViewModel>();
        public TreesUwpViewModel TreesUwpViewModel => _container.Resolve<TreesUwpViewModel>();
        public CanvasUwpViewModel CanvasUwpViewModel => _container.Resolve<CanvasUwpViewModel>();
    }
}
