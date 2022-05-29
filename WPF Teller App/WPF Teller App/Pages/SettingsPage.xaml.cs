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

using Bank_Db_Class_Library;

namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page, IPageClosingHandler
    {
        public int bankId;

        public AdminPage adminPage;

        public SettingsPage()
        {
            InitializeComponent();
        }

        public void ClearAllFields()
        {
            BankNameTextBox.Text = string.Empty;
            BankIdLabel.Content = string.Empty;
        }

        public bool Close()
        {
            return true;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (BankNameTextBox.Text is not null)
            {
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
#warning FirstOrDefault can return int.default -> May cause errors!
                    bankId = dbContext.Banks.Where(b => b.Name == BankNameTextBox.Text).FirstOrDefault().BankId;
                }
                BankIdLabel.Content = $"Bank Id: {bankId}";
                adminPage.bankId = bankId;
            }
        }
    }
}
