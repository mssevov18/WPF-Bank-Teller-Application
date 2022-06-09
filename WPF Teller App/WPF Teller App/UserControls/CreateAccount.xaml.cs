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
using Bank_Db_Class_Library;
using System.Text.Json;

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
        public CreateAccount(int bankId)
        {
            this.bankId = bankId;
            InitializeComponent();
        }

        public int bankId;

        public string AccountIban { get => IBANTextBox.Text; }
        public string Email { get => EmailTextBox.Text; }
        public string Password { get => PasswordTextBox.Password; }
        public string PasswordConfirm { get => PasswordConfirmTextBox.Password; }
        public DateTime CreationDate { get => DateTime.Now; }
        public string Balance { get => BalanceTextBox.Text; }

        public Person PersonNavigation { get => this.GetPerson(); }

        public Person GetPerson()
        {
            return CreatePersonUserControl.GetPerson();
        }

        public Account GetAccount()
        {
            return new Account();
        }

        public bool AreAllFieldsEmpty()
        {
            return CreatePersonUserControl.AreAllFieldsEmpty() &&
                   IBANTextBox.Text == string.Empty &&
                   EmailTextBox.Text == string.Empty &&
                   PasswordTextBox.Password == string.Empty &&
                   PasswordConfirmTextBox.Password == string.Empty &&
                   BalanceTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            CreatePersonUserControl.ClearAllFields();
            IBANTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PasswordTextBox.Password = string.Empty;
            PasswordConfirmTextBox.Password = string.Empty;
            BalanceTextBox.Text = string.Empty;

            DefaultPasswordTextBox();
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

        public void Submit()
        {
            try
            {
                // Send Request!
#warning Bank, Person and Account are all nullable and may return null. Investigate!
                Bank? testBank;
                Person? testPerson;
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    testBank = dbContext.Banks.Where(bank => bank.BankId == Bank_DatabaseContext.BankId).FirstOrDefault();
                    testPerson = dbContext.People.Where(person => person.Egn == CreatePersonUserControl.EGN).FirstOrDefault();
                }

                if (testBank is null)
                {
                    MessageBox.Show("Bank not here");
                    return;
                    //throw new Exception("Bank not here");
                }

                Person person = null;
                if (testPerson is null)
                    person = CreatePersonUserControl.GetPerson();

                Account account = new Account(
                    IBANTextBox.Text,
                    decimal.Parse(BalanceTextBox.Text),
                    Bank_DatabaseContext.BankId,
                    CreatePersonUserControl.EGN,
                    DateTime.Now,
                    EmailTextBox.Text,
                    PasswordTextBox.Password);

                string serializedPerson = JsonSerializer.Serialize<Person>(person);
                string serializedAccount = JsonSerializer.Serialize<Account>(account);

                // Serialize person and account to json (:
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    if (person != null)
                        dbContext.Requests.Add(new Request(
                            "T",
                            DateTime.Now,
                            null,
                            "Person",
                            null,
                            serializedPerson));

                    if (account != null)
                        dbContext.Requests.Add(new Request(
                            "T",
                            DateTime.Now,
                            null,
                            "Account",
                            null,
                            serializedAccount));
                    dbContext.SaveChanges();


                    this.ClearAllFields();
                    ///TODO_HIGH: Async check if the request is handled and open a msgBox
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.ToString()}\n\n{exception.Message}\n\n{exception.StackTrace}\n\n{exception.HelpLink}", $"{exception.GetType().ToString()}");
            }
        }
    }
}
