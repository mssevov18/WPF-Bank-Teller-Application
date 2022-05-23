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
    /// Interaction logic for WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page, IPageClosingHandler
    {
        public WorkerPage()
        {
            InitializeComponent();
        }

        public void ClearAllFields()
        {
            //throw new NotImplementedException();
        }

        public bool Close()
        {
            // Handle user operations.
            // If user has unsaved work ask if they want to close
            return true;
        }
    }
}
