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

namespace WPF_Teller_App.UserControls.AccountControls
{
    /// <summary>
    /// Interaction logic for FindAccount.xaml
    /// </summary>
    public partial class FindAccount : UserControl, IUserControlFormHandler
    {
        public ShowAccount showAccount;
        public ListAccounts listAccounts;

        public FindAccount()
        {
            InitializeComponent();

            showAccount = new ShowAccount();
            listAccounts = new ListAccounts();
            ShowAccountFrame.Content = null;

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

            showAccount.ClearAllFields();
            listAccounts.ClearAllFields();
            ShowAccountFrame.Content = null;
        }

        public void Submit()
        {
            try
            {
                Account account = null;
                Person person = null;
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
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
                    showAccount.Account = account;
                if (person is not null)
                    showAccount.Person = person;
                ShowAccountFrame.Content = showAccount;
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

        private void ShowListButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAccountFrame.Content = listAccounts;
            listAccounts.Submit();
        }
    }
}
