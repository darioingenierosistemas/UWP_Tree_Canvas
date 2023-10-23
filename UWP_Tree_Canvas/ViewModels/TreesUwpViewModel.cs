using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UWP_Tree_Canvas.Base;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using mux = Microsoft.UI.Xaml.Controls;

namespace UWP_Tree_Canvas.ViewModels
{
    public class TreesUwpViewModel : ViewModelBase
    {

        private const string URL = "https://jsonplaceholder.typicode.com";
        public ObservableCollection<Users> Items;

        public void JsonPlaceHorlder(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{url}/users");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<Users> usersList = JsonConvert.DeserializeObject<List<Users>>(data);
                Items = new ObservableCollection<Users>(usersList);

                foreach (Users user in usersList)
                {
                    mux.TreeViewNode node = new mux.TreeViewNode();
                    node.Content = user.name;
                    node.IsExpanded = true;


                    // Crear nodos TreeView hijos para la dirección y la compañía del usuario
                    if (user.address != null)
                    {
                        mux.TreeViewNode addressNode = new mux.TreeViewNode();
                        addressNode.Content = user.address.city;
                        node.Children.Add(addressNode);
                    }

                    if (user.company != null)
                    {
                        mux.TreeViewNode companyNode = new mux.TreeViewNode();
                        companyNode.Content = user.company.name;
                        node.Children.Add(companyNode);

                    }

                    // Añadir el nodo TreeView padre al TreeView
                    TreeViewUsers.RootNodes.Add(node);


                }
            }


        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            JsonPlaceHorlder(URL);
            return null;
        }
    }

  
}
