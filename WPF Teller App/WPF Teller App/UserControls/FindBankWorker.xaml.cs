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
    /// Interaction logic for FindBankWorker.xaml
    /// </summary>
    public partial class FindBankWorker : UserControl, IUserControlFormHandler
    {
        private BankWorker worker;
        private Person person;

        public FindBankWorker()
        {
            InitializeComponent();

            UsernameRadioButton.IsChecked = false;
            EGNRadioButton.IsChecked = false;

            UsernameTextBox.IsEnabled = false;
            EGNTextBox.IsEnabled = false;
        }

        public bool AreAllFieldsEmpty()
        {
            return UsernameTextBox.Text == string.Empty &&
                   EGNTextBox.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            UsernameTextBox.Text = string.Empty;
            EGNTextBox.Text = string.Empty;


            UsernameRadioButton.IsChecked = false;
            EGNRadioButton.IsChecked = false;

            UsernameTextBox.IsEnabled = false;
            EGNTextBox.IsEnabled = false;

            UsernameLabel.Content = string.Empty;
            NameLabel.Content = string.Empty;
            ResidenceLabel.Content = string.Empty;
            BirthDateLabel.Content = string.Empty;
            EGNLabel.Content = string.Empty;
            IsAdminLabel.Content = "IsAdmin?";
        }

        public void Submit()
        {
            try
            {
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    worker = null;
                    person = null;
                    if (UsernameRadioButton.IsChecked == true)
                    {
                        worker = dbContext.BankWorkers.Where(bw => bw.Username == UsernameTextBox.Text).FirstOrDefault();
                        if (worker is null)
                            throw new Exception($"Username was not found");
                    }
                    else if (EGNRadioButton.IsChecked == true)
                    {
                        worker = dbContext.BankWorkers.Where(bw => bw.PersonEgn == EGNTextBox.Text).FirstOrDefault();
                        if (worker is null)
                            throw new Exception($"EGN was not found");
                    }
                    else
                        return;

                    person = dbContext.People.Where(p => p.Egn == worker.PersonEgn).FirstOrDefault();
                }
                if (worker is not null)
                {
                    UsernameLabel.Content = worker.Username;
                    IsAdminLabel.Content = worker.IsAdmin ? "Admin" : "Worker";
                    EGNLabel.Content = worker.PersonEgn;
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

        private void EGNRadioButton_Click(object sender, RoutedEventArgs e)
        {
            EGNTextBox.IsEnabled = true;
            UsernameTextBox.IsEnabled = false;

            EGNRadioButton.IsChecked = true;
            UsernameRadioButton.IsChecked = false;

            EGNTextBox.Focus();
        }

        private void UsernameRadioButton_Click(object sender, RoutedEventArgs e)
        {
            EGNTextBox.IsEnabled = false;
            UsernameTextBox.IsEnabled = true;

            UsernameRadioButton.IsChecked = true;
            EGNRadioButton.IsChecked = false;

            UsernameTextBox.Focus();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Submit();
        }
    }
}
