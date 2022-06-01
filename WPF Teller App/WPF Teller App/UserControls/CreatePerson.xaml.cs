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
        }
    }
}
