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

using Bank_Db_Class_Library;

///TODO_HIGH: Add regional settings and such to assist in IBAN Generation
///TODO_HIGH: Move bankId to arguments

namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page, IPageClosingHandler
    {
        public AdminPage adminPage;
        public WorkerPage workerPage;

        public SettingsPage()
        {
            this.Title = "Settings";

            InitializeComponent();
        }

#warning Not implemented "public void ClearAllFields()"
        public void ClearAllFields()
        {
            //BankNameTextBox.Text = string.Empty;
            //BankIdLabel.Content = string.Empty;
        }

#warning Not implemented "public bool Close()"
        public bool Close()
        {
            return true;
        }

#warning Not implemented "public bool IsMainFieldEmpty()"
        public bool IsMainFieldEmpty()
        {
            return true;
        }

        //        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        //        {
        //            if (BankNameTextBox.Text != string.Empty)
        //            {
        //                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
        //                {
        //#warning FirstOrDefault can return int.default -> May cause errors!
        //                    Bank bank = dbContext.Banks.Where(b => b.Name == BankNameTextBox.Text).FirstOrDefault();
        //                    if (bank is not null)
        //                        bankId = bank.BankId;
        //                    else
        //                        bankId = 0;
        //                }
        //                BankIdLabel.Content = $"Bank Id: {bankId}";
        //                adminPage.bankId = bankId;
        //                workerPage.bankId = bankId;
        //            }
        //        }
    }
}
