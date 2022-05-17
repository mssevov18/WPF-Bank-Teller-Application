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

namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Interaction logic for MainPageNonAdmin.xaml
    /// </summary>
    public partial class MainPageNonAdmin : Page
    {
        public static int PageWidth => 800;
        public static int PageHeigth => 450;

        public MainPageNonAdmin()
        {
            InitializeComponent();
        }
    }
}
