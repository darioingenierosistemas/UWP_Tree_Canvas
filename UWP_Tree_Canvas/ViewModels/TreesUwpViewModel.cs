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
         private ObservableCollection<ExplorerItem> dataSourceTreeView;

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

        public ObservableCollection<ExplorerItem> DataSourceTreeView
        {
            get { return dataSourceTreeView; }
            set
            {
                if (dataSourceTreeView != value)
                {
                    dataSourceTreeView = value;
                }
            }
        }

        private ObservableCollection<ExplorerItem> GetData()
        {
            var list = new ObservableCollection<ExplorerItem>();
            ExplorerItem folder1 = new ExplorerItem()
            {
                Name = "Work Documents",
                Type = ExplorerItem.ExplorerItemType.Folder,
                Children =
            {
                new ExplorerItem()
                {
                    Name = "Functional Specifications",
                    Type = ExplorerItem.ExplorerItemType.Folder,
                    Children =
                    {
                        new ExplorerItem()
                        {
                            Name = "TreeView spec",
                            Type = ExplorerItem.ExplorerItemType.File,
                          }
                    }
                },
                new ExplorerItem()
                {
                    Name = "Feature Schedule",
                    Type = ExplorerItem.ExplorerItemType.File,
                },
                new ExplorerItem()
                {
                    Name = "Overall Project Plan",
                    Type = ExplorerItem.ExplorerItemType.File,
                },
                new ExplorerItem()
                {
                    Name = "Feature Resources Allocation",
                    Type = ExplorerItem.ExplorerItemType.File,
                }
            }
            };
            ExplorerItem folder2 = new ExplorerItem()
            {
                Name = "Personal Folder",
                Type = ExplorerItem.ExplorerItemType.Folder,
                Children =
                    {
                        new ExplorerItem()
                        {
                            Name = "Home Remodel Folder",
                            Type = ExplorerItem.ExplorerItemType.Folder,
                            Children =
                            {
                                new ExplorerItem()
                                {
                                    Name = "Contractor Contact Info",
                                    Type = ExplorerItem.ExplorerItemType.File,
                                },
                                new ExplorerItem()
                                {
                                    Name = "Paint Color Scheme",
                                    Type = ExplorerItem.ExplorerItemType.File,
                                },
                                new ExplorerItem()
                                {
                                    Name = "Flooring Woodgrain type",
                                    Type = ExplorerItem.ExplorerItemType.File,
                                },
                                new ExplorerItem()
                                {
                                    Name = "Kitchen Cabinet Style",
                                    Type = ExplorerItem.ExplorerItemType.File,
                                }
                            }
                        }
                    }
            };

            list.Add(folder1);
            list.Add(folder2);
            return list;
        }

        public void Tree_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Delete)
            {
                DeleteSelectItem(DataSourceTreeView);
            }

        }
        public ExplorerItem DeleteSelectItem(ObservableCollection<ExplorerItem> DataSource)
        {
            foreach (var item in DataSource)
            {
                if (item.IsSelected == true)
                {
                    DataSource.Remove(item);
                    return item;
                }

                var FindResult = DeleteSelectItem(item.Children);
                if (FindResult != null)
                    return FindResult;
            }

            return null;
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            //JsonPlaceHorlder(URL);
            DataSourceTreeView = GetData();
            return null;
        }


    }

    public class ExplorerItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public enum ExplorerItemType { Folder, File };
        public String Name { get; set; }
        public ExplorerItemType Type { get; set; }
        private ObservableCollection<ExplorerItem> m_children;
        public ObservableCollection<ExplorerItem> Children
        {
            get
            {
                if (m_children == null)
                {
                    m_children = new ObservableCollection<ExplorerItem>();
                }
                return m_children;
            }
            set
            {
                m_children = value;
            }
        }

        private bool m_isExpanded;
        public bool IsExpanded
        {
            get { return m_isExpanded; }
            set
            {
                if (m_isExpanded != value)
                {
                    m_isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        private bool m_isSelected;
        public bool IsSelected
        {
            get { return m_isSelected; }

            set
            {
                if (m_isSelected != value)
                {
                    m_isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }

        }

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


        public class ExplorerItemTemplateSelector : DataTemplateSelector
        {
            public DataTemplate FolderTemplate { get; set; }
            public DataTemplate FileTemplate { get; set; }

            protected override DataTemplate SelectTemplateCore(object item)
            {
                var explorerItem = (ExplorerItem)item;
                return explorerItem.Type == ExplorerItem.ExplorerItemType.Folder ? FolderTemplate : FileTemplate;
            }
        }

}
