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
using WPF_Teller_App.Interfaces;

namespace WPF_Teller_App.UserControls
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : UserControl, IUserControlFormHandler
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public bool AreAllFieldsEmpty()
        {
            throw new NotImplementedException();
        }

        public void ClearAllFields()
        {
            throw new NotImplementedException();
        }
    }
}
