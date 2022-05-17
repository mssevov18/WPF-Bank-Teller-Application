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
using System.Security.Cryptography;
using WPF_Teller_App.Data.Models;
using WPF_Teller_App.Pages;
using Microsoft.EntityFrameworkCore;

namespace WPF_Teller_App
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public static int PageWidth => 450;
        public static int PageHeigth => 200;

        public MainWindow MainWindow { get; set; }
        public MainPageNonAdmin MainPage { get; set; }

        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            // Maybe??
            // ??Check if username is ok and password is ok??
            if (LogIn(UsernameTextBox.Text, PasswordTextBox.Password) is not null)
                MainWindow.ChangePage(MainPage, MainPageNonAdmin.PageWidth, MainPageNonAdmin.PageHeigth);
            else
                this.Background = Brushes.Red;
        }

        private BankWorker LogIn(string username, string unhashed_password)
        {
            BankWorker empty;
            //Check that neither username nor password textboxes are empty
            using (Bank_DatabaseContext bankDbContext = new Bank_DatabaseContext())
            {
                empty = bankDbContext.BankWorkers.Where(
                    worker => worker.Username == username &&
                              worker.Password == unhashed_password)
                              // Hash password in the future
                              //worker.Password == HashPassword(unhashed_password, username))
                    .FirstOrDefault();
                if (empty is not null)
                    return empty;
            }
            return null;
        }

        private string HashPassword(string unhashed_password, string salt)
        {
            return Encoding.ASCII.GetString(
                        SHA256.Create().ComputeHash(
                            Encoding.ASCII.GetBytes(unhashed_password)));
                            //Encoding.ASCII.GetBytes(unhashed_password + salt)));
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

// ПАЙН ГАЛОРЕ
//    string hashedPassword = HashPassword(PasswordTextBox.Password, UsernameTextBox.Text);
//    using (Bank_DatabaseContext bankDbContext = new Bank_DatabaseContext())
//    {
//        Bank bank = new Bank(10, "testing");
//        Person person = new Person("1234567890", "Martin", "Stefanov",
//                                   "Sevov", "Slaveikov", new DateTime(2004, 2, 23));
//        BankWorker bankWorker = new BankWorker("admin", HashPassword("admin","admin"), 
//                                               true, 1000, 10, "1234567890");

//        //bank.BankWorkers.Add(bankWorker);
//        //person.BankWorkers.Add(bankWorker);

//        //bankWorker.Bank = bank;
//        //bankWorker.PersonEgnNavigation = person;

//        // OLD ERRORRRR
//        // No suitable constructor was found for entity type 'BankWorker'
//        bankDbContext.Banks.Add(bank);
//        bankDbContext.People.Add(person);
//        bankDbContext.BankWorkers.Add(bankWorker);

//        bankDbContext.SaveChanges();
//    }
//}