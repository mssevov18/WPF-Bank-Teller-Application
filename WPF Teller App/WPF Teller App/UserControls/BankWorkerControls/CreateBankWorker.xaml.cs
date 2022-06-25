using Bank_Db_Class_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using WPF_Teller_App.UserControls.PersonControls;

namespace WPF_Teller_App.UserControls.BankWorkerControls
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

            DefaultPasswordTextBox();
        }

        public void Submit()
        {
            try
            {
                // Send Request!
#warning Bank, Person and BankWorker are all nullable and may return null. Investigate!
                Bank? testBank;
                Person? testPerson;
                BankWorker? testWorker;
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    testBank = dbContext.Banks.Where(bank => bank.BankId == Bank_DatabaseContext.BankId).FirstOrDefault();
                    testPerson = dbContext.People.Where(person => person.Egn == PersonUserControl.EGN).FirstOrDefault();
                }

                if (testBank is null)
                {
                    MessageBox.Show("Bank not here");
                    return;
                    //throw new Exception("Bank not here");
                }

                Person person = null;
                if (testPerson is null)
                    person = PersonUserControl.GetPerson();

                BankWorker bankWorker = new BankWorker(
                    Username,
                    Password,
                    IsAdmin,
                    decimal.Parse(Salary),
                    Bank_DatabaseContext.BankId,
                    PersonUserControl.EGN);

                string serializedPerson = JsonSerializer.Serialize<Person>(person);
                string serializedBankWorker = JsonSerializer.Serialize<BankWorker>(bankWorker);

                // Serialize person and bankworker to json (:
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

                    if (bankWorker != null)
                        dbContext.Requests.Add(new Request(
                            "T",
                            DateTime.Now,
                            null,
                            "Bank_Worker",
                            null,
                            serializedBankWorker));
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
