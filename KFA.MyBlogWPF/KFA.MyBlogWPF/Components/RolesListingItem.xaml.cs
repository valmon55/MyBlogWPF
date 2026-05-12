using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KFA.MyBlogWPF.Components
{
    /// <summary>
    /// Логика взаимодействия для RolesListingItem.xaml
    /// </summary>
    public partial class RolesListingItem : UserControl
    {
        public RolesListingItem()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
