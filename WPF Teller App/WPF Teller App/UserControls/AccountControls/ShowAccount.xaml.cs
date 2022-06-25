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
    /// Interaction logic for ShowAccount.xaml
    /// </summary>
    public partial class ShowAccount : UserControl, IUserControlFormHandler
    {
        ///TODO_HIGH: Finish this class
        Account account;
        public Account Account
        { 
            get => account;
            set
            {
                account = value;

                EmailLabel.Content = account.Email;
                BalanceLabel.Content = account.Balance;
                EGNLabel.Content = account.PersonEgn;
                IBANLabel.Content = account.AccountIban;
            }
        }

        Person person;
        public Person Person
        {
            get => person;
            set
            {
                person = value;

                NameLabel.Content = $"{person.FirstName} {person.MiddleName} {person.LastName}";
                ResidenceLabel.Content = $"{person.Residence}";
                BirthDateLabel.Content = $"{person.BirthDay}";
            } 
        }

        public ShowAccount()
        {
            InitializeComponent();
        }

        public bool AreAllFieldsEmpty()
        {
            return true;
        }

        public void ClearAllFields()
        {
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
            return;
        }
    }
}
