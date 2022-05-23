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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page, IPageClosingHandler
    {
        public int bankId;

        public AdminPage()
        {
            InitializeComponent();
        }

        public bool Close()
        {
            if (!AnyFieldsIsEmpty())
                return MessageBox.Show("You have unsaved changes!. Press Ok to discard them.",
                                       "Unsaved Changes!", MessageBoxButton.OKCancel)
                    == MessageBoxResult.OK;
            return true;
        }

        private bool AnyFieldsIsEmpty()
        {
            return UsernameTextBox.Text == string.Empty &&
                   PasswordTextBox.Password == string.Empty &&
                   PasswordConfirmTextBox.Password == string.Empty &&
                   SalaryTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            UsernameTextBox.Text = string.Empty;
            SalaryTextBox.Text = string.Empty;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
