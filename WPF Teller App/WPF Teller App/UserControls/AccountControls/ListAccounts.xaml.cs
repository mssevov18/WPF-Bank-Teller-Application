using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPF_Teller_App.Interfaces;

namespace WPF_Teller_App.UserControls.AccountControls
{
    public class AccountPOCO
    {
        public AccountPOCO()
        {
            account = null;
            person = null;
        }
        public AccountPOCO(Account account)
        {
            this.account = account;
        }

        public Account account;
        public Person person;

        public string FullName { get => person.FullName; }
        public string Egn { get => account.PersonEgn; }
        public decimal Balance { get => account.Balance; }
        public string Email { get => account.Email; }
        public string Iban { get => account.AccountIban; }

        public override string ToString() => $"Name:    {FullName}\n" +
                                             $"Egn:     {Egn}\n" +
                                             $"Balance: {Balance}\n" +
                                             $"Email:   {Email}\n" +
                                             $"Iban:    {Iban}";
    }

    /// <summary>
    /// Interaction logic for ListAccounts.xaml
    /// </summary>
    public partial class ListAccounts : UserControl, IUserControlFormHandler
    {
        ///TODO_HIGH: Finish this class

        ObservableCollection<AccountPOCO> pocoAccounts;

        public ListAccounts()
        {
            pocoAccounts = new ObservableCollection<AccountPOCO>();

            InitializeComponent();

            AccountListBox.DataContext = pocoAccounts;
            AccountListBox.ItemsSource = pocoAccounts;
        }

        public bool AreAllFieldsEmpty()
        {
            return true;
        }

        public void ClearAllFields()
        {
            pocoAccounts.Clear();
        }

        public void Submit()
        {
            //Search db for accounts
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                pocoAccounts.Clear();

                ///TODO_HIGH: find data
                foreach (Account account in dbContext.Accounts)
                    pocoAccounts.Add(new AccountPOCO(account));

                foreach(AccountPOCO poco in pocoAccounts)
                    poco.person = dbContext.People.Where(p => p.Egn == poco.account.PersonEgn).FirstOrDefault();
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(AccountListBox.SelectedItem.ToString(), "Expanded data", MessageBoxButton.OK);
        }
    }
}
