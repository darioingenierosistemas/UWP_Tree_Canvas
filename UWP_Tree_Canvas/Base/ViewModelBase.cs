using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWP_Tree_Canvas.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using mux = Microsoft.UI.Xaml.Controls;

namespace UWP_Tree_Canvas.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Frame _appFrame;
        public Frame AppFrame => _appFrame;

        private Frame _splitViewFrame;
        public Frame SplitViewFrame => _splitViewFrame;

        public mux.TreeView _treeViewUsers;
        public mux.TreeView TreeViewUsers => _treeViewUsers;

        public Page _page;
        public Page Page => _page;

        public abstract Task OnNavigatedTo(NavigationEventArgs args);
        public abstract Task OnNavigatedFrom(NavigationEventArgs args);

        internal void SetAppFrame(Frame viewFrame)
        {
            _appFrame = viewFrame;
        }

        internal void SetSplitViewFrame(Frame viewFrame)
        {
            _splitViewFrame = viewFrame;
        }

        internal void SetTreeViewUsers(mux.TreeView viewTreeView)
        {
            _treeViewUsers = viewTreeView;
        }

        internal void SetPage(Page viewPage)
        {
            _page = viewPage;
        }
    }
}
