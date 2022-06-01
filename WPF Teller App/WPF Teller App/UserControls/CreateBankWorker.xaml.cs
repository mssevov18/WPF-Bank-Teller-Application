using Bank_Db_Class_Library;
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
    /// Interaction logic for CreateBankWorker.xaml
    /// </summary>
    public partial class CreateBankWorker : UserControl, IUserControlFormHandler
    {
        public CreateBankWorker()
        {
            InitializeComponent();
        }

        public string Username { get => UsernameTextBox.Text; }
        public string Password { get => PasswordTextBox.Password; }
        public string PasswordConfirm { get => PasswordConfirmTextBox.Password; }
        public bool IsAdmin { get => IsAdminCheckBox.IsChecked.GetValueOrDefault(); }
        public string Salary { get => SalaryTextBox.Text; }
        public CreatePerson PersonUserControl { get => CreatePersonUserControl; }

        public bool AreAllFieldsEmpty()
        {
            return CreatePersonUserControl.AreAllFieldsEmpty() &&
                   UsernameTextBox.Text == string.Empty &&
                   PasswordTextBox.Password == string.Empty &&
                   PasswordConfirmTextBox.Password == string.Empty &&
                   SalaryTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            CreatePersonUserControl.ClearAllFields();

            UsernameTextBox.Text = string.Empty;
            SalaryTextBox.Text = string.Empty;
            PasswordTextBox.Password = string.Empty;
            PasswordConfirmTextBox.Password = string.Empty;
            IsAdminCheckBox.IsChecked = false;
        }

        public void DefaultPasswordTextBox()
        {
            PasswordConfirmTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (PasswordTextBox.Password == string.Empty || PasswordConfirmTextBox.Password == string.Empty)
            {
                PasswordConfirmTextBox.Background = Brushes.White;
                PasswordTextBox.Background = Brushes.White;
            }
            else if (PasswordConfirmTextBox.Password != PasswordTextBox.Password)
            {
                PasswordConfirmTextBox.Background = Brushes.Red;
                PasswordTextBox.Background = Brushes.Red;
            }
            else
            {
                PasswordConfirmTextBox.Background = Brushes.Green;
                PasswordTextBox.Background = Brushes.Green;
            }
        }
    }
}
