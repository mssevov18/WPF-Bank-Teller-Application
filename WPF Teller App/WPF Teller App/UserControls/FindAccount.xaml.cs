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
    /// Interaction logic for FindAccount.xaml
    /// </summary>
    public partial class FindAccount : UserControl, IUserControlFormHandler
    {
        private Account account;
        private Person person;

        public FindAccount()
        {
            InitializeComponent();

            EmailRadioButton.IsChecked = false;
            EGNRadioButton.IsChecked = false;

            EmailTextBox.IsEnabled = false;
            EGNTextBox.IsEnabled = false;
        }

        public bool AreAllFieldsEmpty()
        {
            return EmailTextBox.Text == string.Empty &&
                   EGNTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            EmailTextBox.Text = string.Empty;
            EGNTextBox.Text = string.Empty;

            EmailRadioButton.IsChecked = false;
            EGNRadioButton.IsChecked = false;

            EmailTextBox.IsEnabled = false;
            EGNTextBox.IsEnabled = false;

            EmailLabel.Content = string.Empty;
            NameLabel.Content = string.Empty;
            ResidenceLabel.Content = string.Empty;
            BirthDateLabel.Content = string.Empty;
            EGNLabel.Content = string.Empty;
            BalanceLabel.Content = string.Empty;
            IBANLabel.Content = string.Empty;
        }

        public void Submit()
        {
            try
            {
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    account = null;
                    person = null;
                    if (EmailRadioButton.IsChecked == true)
                    {
                        account = dbContext.Accounts.Where(bw => bw.Email == EmailTextBox.Text).FirstOrDefault();
                        if (account is null)
                            throw new Exception($"Email was not found");
                    }
                    else if (EGNRadioButton.IsChecked == true)
                    {
                        account = dbContext.Accounts.Where(bw => bw.PersonEgn == EGNTextBox.Text).FirstOrDefault();
                        if (account is null)
                            throw new Exception($"EGN was not found");
                    }
                    else
                        return;

                    person = dbContext.People.Where(p => p.Egn == account.PersonEgn).FirstOrDefault();
                }
                if (account is not null)
                {
                    EmailLabel.Content = account.Email;
                    BalanceLabel.Content = account.Balance;
                    EGNLabel.Content = account.PersonEgn;
                    IBANLabel.Content = account.AccountIban;
                }
                if (person is not null)
                {
                    NameLabel.Content = $"{person.FirstName} {person.MiddleName} {person.LastName}";
                    ResidenceLabel.Content = $"{person.Residence}";
                    BirthDateLabel.Content = $"{person.BirthDay}";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void EmailRadioButton_Click(object sender, RoutedEventArgs e)
        {
            EGNTextBox.IsEnabled = false;
            EmailTextBox.IsEnabled = true;

            EmailRadioButton.IsChecked = true;
            EGNRadioButton.IsChecked = false;

            EmailTextBox.Focus();
        }

        private void EGNRadioButton_Click(object sender, RoutedEventArgs e)
        {
            EGNTextBox.IsEnabled = true;
            EmailTextBox.IsEnabled = false;

            EGNRadioButton.IsChecked = true;
            EmailRadioButton.IsChecked = false;

            EGNTextBox.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Submit();
        }
    }
}
