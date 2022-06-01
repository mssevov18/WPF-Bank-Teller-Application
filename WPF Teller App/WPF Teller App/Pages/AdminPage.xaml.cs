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

///IDEA: Add salt and hashed passwords

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
            {
                if (MessageBox.Show("You have unsaved changes!. Press Ok to discard them.",
                                      "Unsaved Changes!", MessageBoxButton.OKCancel)
                   == MessageBoxResult.OK)
                    CreateBankWorkerUserControl.DefaultPasswordTextBox();
                else
                    return false;
            }
            return true;
        }

        private bool AnyFieldsIsEmpty()
        {
            //A bit long innit?
            return CreateBankWorkerUserControl.AreAllFieldsEmpty();
        }

        public void ClearAllFields()
        {
            CreateBankWorkerUserControl.ClearAllFields();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ///TODO_HIGH: Add Data checking!!!
            ///       Is data ok?
            ///       Do passwords match?
            ///       Does egn exist?

            try
            {


            // Send Request!
#warning Bank, Person and BankWorker are all nullable and may return null. Investigate!
            Bank? testBank;
            Person? testPerson;
            BankWorker? testWorker;
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                testBank = dbContext.Banks.Where(bank => bank.BankId == bankId).FirstOrDefault();
                testPerson = dbContext.People.Where(person => person.Egn == CreateBankWorkerUserControl.PersonUserControl.EGN).FirstOrDefault();
            }

            if (testBank is null)
            {
                MessageBox.Show("Bank not here");
                return;
                //throw new Exception("Bank not here");
            }

            Person person = null;
            if (testPerson is null)
                person = CreateBankWorkerUserControl.PersonUserControl.GetPerson();

            BankWorker bankWorker = new BankWorker(
                CreateBankWorkerUserControl.Username,
                CreateBankWorkerUserControl.Password,
                CreateBankWorkerUserControl.IsAdmin,
                decimal.Parse(CreateBankWorkerUserControl.Salary),
                bankId,
                CreateBankWorkerUserControl.PersonUserControl.EGN);

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

                ///TODO: Freeze App while adding the Worker and Person
            }

                //MessageBox.Show($"Bank: {JsonSerializer.Serialize<Bank>(bank)}\n" +
                //                $"Person: {JsonSerializer.Serialize<Person>(person)}\n" +
                //                $"BankWorker: {JsonSerializer.Serialize<BankWorker>(bankWorker)}");

            }
            catch(Exception exception)
            {
                MessageBox.Show($"{exception.ToString()}\n\n{exception.Message}\n\n{exception.StackTrace}\n\n{exception.HelpLink}", $"{exception.GetType().ToString()}");
            }

        }
    }
}
