using UWP_Tree_Canvas.Base;
using UWP_Tree_Canvas.ViewModels;
using Microsoft.UI.Xaml.Controls;
// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Tree_Canvas.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class TreesUwpView : PageBase
    {

        public TreesUwpView()
        {
            this.InitializeComponent();
            //base.Page = this;
            base.TreeViewUsers = treeViewUsers;

        }

    }

   
}
