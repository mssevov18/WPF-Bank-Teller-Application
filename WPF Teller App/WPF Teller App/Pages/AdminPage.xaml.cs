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

/*
TODO_HIGH: Test PasswordCheckBox against PasswordBox.
IDEA: Add salt and hashed passwords

 
*/

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
            //A bit long innit?
            return FirstnameTextBox.Text == string.Empty &&
                   MiddlenameTextBox.Text == string.Empty &&
                   LastnameTextBox.Text == string.Empty &&
                   EGNTextBox.Text == string.Empty &&
                   AddressTextBox.Text == string.Empty &&
                   BirthDatePicker.Text == string.Empty &&
                   UsernameTextBox.Text == string.Empty &&
                   PasswordTextBox.Password == string.Empty &&
                   PasswordConfirmTextBox.Password == string.Empty &&
                   SalaryTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            // Personal Data
            FirstnameTextBox.Text = string.Empty;
            MiddlenameTextBox.Text = string.Empty;
            LastnameTextBox.Text = string.Empty;
            EGNTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            BirthDatePicker.Text = string.Empty;

            // Worker Data
            UsernameTextBox.Text = string.Empty;
            SalaryTextBox.Text = string.Empty;
            IsAdminCheckBox.IsChecked = false;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO_HIGH: Add Data checking!!!
            //       Is data ok?
            //       Do passwords match?
            //       Does egn exist?

            // Send Request!
            Bank? testBank;
            Person? testPerson;
            BankWorker? testWorker;
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                testBank = dbContext.Banks.Where(bank => bank.BankId == bankId).FirstOrDefault();
                testPerson = dbContext.People.Where(person => person.Egn == EGNTextBox.Text).FirstOrDefault();
            }

            if (testBank is null)
            {
                MessageBox.Show("Bank not here");
                return;
                //throw new Exception("Bank not here");
            }

            Person person = null;
            if (testPerson is null)
            {
                person = new Person(
                      EGNTextBox.Text,
                      FirstnameTextBox.Text,
                      MiddlenameTextBox.Text,
                      LastnameTextBox.Text,
                      AddressTextBox.Text,
                      BirthDatePicker.SelectedDate.GetValueOrDefault());
            }

            BankWorker bankWorker = new BankWorker(
                UsernameTextBox.Text,
                PasswordTextBox.Password,
                IsAdminCheckBox.IsChecked.GetValueOrDefault(),
                decimal.Parse(SalaryTextBox.Text),
                bankId,
                EGNTextBox.Text);

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
                        JsonSerializer.Serialize<Person>(person)));
                dbContext.Requests.Add(new Request(
                    "T",
                    DateTime.Now,
                    null,
                    "Bank_Worker",
                    null,
                    JsonSerializer.Serialize<BankWorker>(bankWorker)));
                dbContext.SaveChanges();
            }

            //MessageBox.Show($"Bank: {JsonSerializer.Serialize<Bank>(bank)}\n" +
            //                $"Person: {JsonSerializer.Serialize<Person>(person)}\n" +
            //                $"BankWorker: {JsonSerializer.Serialize<BankWorker>(bankWorker)}");


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
