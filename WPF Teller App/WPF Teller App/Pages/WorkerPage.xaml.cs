using Bank_Db_Class_Library;
using System;
using System.Collections;
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
using WPF_Teller_App.Pages;
using WPF_Teller_App.UserControls;

///TODO_HIGH: Make AddAccount
///TODO_HIGH: Make ChangeAccount
///TODO_HIGH: Make FindAccount
///TODO_HIGH: Make RemoveAccount

///TODO: Make AddCard
///TODO: Make ChangeCard
///TODO: Make FindCard
///TODO: Make RemoveCard

///TODO: Make AddCardReader
///TODO: Make ChangeCardReader
///TODO: Make FindCardReader
///TODO: Make RemoveCardReader

namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Interaction logic for WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page, IPageClosingHandler
    {
        //userControls["AA"] -> Account Add user control
        private Dictionary<string, IUserControlFormHandler> ucDictionary;

        public WorkerPage()
        {
            // Add All user controlls to the list for easier manipulation
            ucDictionary = new Dictionary<string, IUserControlFormHandler>();
            ucDictionary.Add("AA", new CreateAccount(Bank_DatabaseContext.BankId));

            InitializeComponent();
        }

        public void ClearAllFields()
        {
            FirstChoiceCombo.SelectedIndex = -1;

            if (ucDictionary is not null)
                foreach (KeyValuePair<string, IUserControlFormHandler> item in ucDictionary)
                    item.Value.ClearAllFields();

        }

#warning Not implemented "public bool Close()"
        public bool Close()
        {
            // Handle user operations.
            // If user has unsaved work ask if they want to close
            return true;
        }

        private void FirstChoiceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///TODO: Ask if it is ok to remove data

            SecondChoiceCombo.SelectedIndex = -1;
            if (FirstChoiceCombo.SelectedIndex != -1)
            {
                SecondChoiceCombo.Visibility = Visibility.Visible;
                SecondChoiceCombo.IsEnabled = true;
                if (((ComboBoxItem)FirstChoiceCombo.SelectedItem).Tag.ToString() == "T")
                {
                    UpdateComboBoxItem.Visibility = Visibility.Collapsed;
                    RemoveComboBoxItem.Visibility = Visibility.Collapsed;
                }
                else
                {
                    UpdateComboBoxItem.Visibility = Visibility.Visible;
                    RemoveComboBoxItem.Visibility = Visibility.Visible;
                }
            }
            else
            {
                SecondChoiceCombo.Visibility = Visibility.Collapsed;
                SecondChoiceCombo.IsEnabled = false;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ///TODO: Add a MessageBox to Ask if it is ok to remove data

            // Clears all forms data
            this.ClearAllFields();
        }

        private void SecondChoiceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (SecondChoiceCombo.SelectedIndex != -1)
            //    MessageBox.Show($"{((ComboBoxItem)FirstChoiceCombo.SelectedItem).Tag}; {((ComboBoxItem)SecondChoiceCombo.SelectedItem).Tag}");

            if (SecondChoiceCombo.SelectedIndex == -1)
                MainContentControl.Content = null;

            if ((ComboBoxItem)SecondChoiceCombo.SelectedItem == null)
                return;

            string key = $"{((ComboBoxItem)FirstChoiceCombo.SelectedItem).Tag.ToString()}{((ComboBoxItem)SecondChoiceCombo.SelectedItem).Tag.ToString()}";
            if (ucDictionary.Where(uc => uc.Key == key).Count() == 1)
                MainContentControl.Content = ucDictionary[key];
            else
                MainContentControl.Content = null;

            //switch ($"{((ComboBoxItem)FirstChoiceCombo.SelectedItem).Tag.ToString()}{((ComboBoxItem)SecondChoiceCombo.SelectedItem).Tag.ToString()}")
            //{
            //    case "AR":
            //        break;
            //    case "AA":
            //        break;
            //    case "AU":
            //        break;
            //    case "AF":
            //        break;

            //    case "TA":
            //        break;
            //    case "TF":
            //        break;

            //    case "CR":
            //        break;
            //    case "CA":
            //        break;
            //    case "CU":
            //        break;
            //    case "CF":
            //        break;

            //    case "RR":
            //        break;
            //    case "RA":
            //        break;
            //    case "RU":
            //        break;
            //    case "RF":
            //        break;

            //    default:
            //        break;
            //}
        }

#warning Not implemented "private void SubmitButton_Click(object sender, RoutedEventArgs e)"
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

        }

#warning Not implemented "public bool IsMainFieldEmpty()"
        public bool IsMainFieldEmpty()
        {
            return true;
        }
    }
}
