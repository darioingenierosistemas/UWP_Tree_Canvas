using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Tree_Canvas.ViewModels;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using mux = Microsoft.UI.Xaml.Controls;

namespace UWP_Tree_Canvas.Base
{
    public class PageBase : Page
    {
        private ViewModelBase _viewModel;

        private Frame _splitViewFrame;
        public Frame SplitViewFrame
        {
            get { return _splitViewFrame; }
            set
            {
                _splitViewFrame = value;
                if (_viewModel == null)
                    _viewModel = (ViewModelBase)this.DataContext;

                _viewModel.SetSplitViewFrame(_splitViewFrame);
            }
        }

        private mux.TreeView _treeViewUsers;

        public mux.TreeView TreeViewUsers
        {
            get { return _treeViewUsers; }
            set
            {
                _treeViewUsers = value;
                if (_viewModel == null)
                    _viewModel = (ViewModelBase)this.DataContext;

                _viewModel.SetTreeViewUsers(_treeViewUsers);
            }
        }

        private Page _page;
        public Page Page
        {
            get { return _page; }
            set
            {
                _page = value;
                if (_viewModel == null)
                    _viewModel = (ViewModelBase)this.DataContext;

                _viewModel.SetPage(_page);
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _viewModel = (ViewModelBase)this.DataContext;
            _viewModel.SetAppFrame(this.Frame);
            _viewModel.OnNavigatedTo(e);

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PageBase_BackRequested;
        }

        private void PageBase_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame != null)
            {
                if (Frame.CanGoBack)
                {
                    e.Handled = true;
                    Frame.GoBack();
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _viewModel.OnNavigatedFrom(e);
        }
    }
}
