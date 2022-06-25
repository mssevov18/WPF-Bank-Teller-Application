using Bank_Db_Class_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
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

namespace WPF_Teller_App.UserControls.PersonControls
{
    /// <summary>
    /// Interaction logic for CreatePerson.xaml
    /// </summary>
    public partial class CreatePerson : UserControl, IUserControlFormHandler
    {
        public CreatePerson()
        {
            InitializeComponent();
        }

        public string EGN { get => EGNTextBox.Text; }
        public string FirstName { get => FirstnameTextBox.Text; }
        public string MiddleName { get => MiddlenameTextBox.Text; }
        public string LastName { get => LastnameTextBox.Text; }
        public string Address { get => AddressTextBox.Text; }
        public DateTime BirthDate { get => BirthDatePicker.SelectedDate.GetValueOrDefault(); }

        public Person GetPerson()
        {
            ///TODO: Check if Person is empty
            return new Person(
                      EGNTextBox.Text,
                      FirstnameTextBox.Text,
                      MiddlenameTextBox.Text,
                      LastnameTextBox.Text,
                      AddressTextBox.Text,
                      BirthDatePicker.SelectedDate.GetValueOrDefault());
        }

        public bool AreAllFieldsEmpty()
        {
            return FirstnameTextBox.Text == string.Empty &&
                   MiddlenameTextBox.Text == string.Empty &&
                   LastnameTextBox.Text == string.Empty &&
                   EGNTextBox.Text == string.Empty &&
                   AddressTextBox.Text == string.Empty &&
                   BirthDatePicker.Text == string.Empty;
        }

        public void ClearAllFields()
        {
            FirstnameTextBox.Text = string.Empty;
            MiddlenameTextBox.Text = string.Empty;
            LastnameTextBox.Text = string.Empty;
            EGNTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            BirthDatePicker.Text = string.Empty;

            FirstnameTextBox.IsEnabled = true;
            MiddlenameTextBox.IsEnabled = true;
            LastnameTextBox.IsEnabled = true;
            AddressTextBox.IsEnabled = true;
            BirthDatePicker.IsEnabled = true;
        }

        public void Submit()
        {
            try
            {
                // Send Request!
#warning Person is nullable and may return null. Investigate!
                Person? testPerson;
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
                    testPerson = dbContext.People.Where(person => person.Egn == EGN).FirstOrDefault();
                //}

                Person person = testPerson;
                if (testPerson is null)
                    person = this.GetPerson();

                // Serialize persons to json (:
                string serializedPerson = JsonSerializer.Serialize<Person>(person);

                //using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                //{
                    if (person != null)
                        dbContext.Requests.Add(new Request(
                            "T",
                            DateTime.Now,
                            null,
                            "Person",
                            null,
                            serializedPerson));

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

        private void EGNTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                if (dbContext.People.Where(p => p.Egn == EGN).FirstOrDefault() != null)
                {
                    FirstnameTextBox.IsEnabled = false; 
                    MiddlenameTextBox.IsEnabled = false;
                    LastnameTextBox.IsEnabled = false;
                    AddressTextBox.IsEnabled = false;
                    BirthDatePicker.IsEnabled = false;
                }
                else
                {
                    FirstnameTextBox.IsEnabled = true;
                    MiddlenameTextBox.IsEnabled = true;
                    LastnameTextBox.IsEnabled = true;
                    AddressTextBox.IsEnabled = true;
                    BirthDatePicker.IsEnabled = true;
                }
            }
        }
    }
}
