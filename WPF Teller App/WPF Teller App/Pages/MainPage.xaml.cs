using System;
using System.IO;
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


namespace WPF_Teller_App.Pages
{
    /// <summary>
    /// Interaction logic for MainPageContainer.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static int PageWidth => 800;
        public static int PageHeigth => 550;

        public MainWindow MainWindow;

        WorkerPage workerPage;
        AdminPage adminPage;
        SettingsPage settingsPage;

        private BankWorker worker;
        public BankWorker Worker
        {
            get => worker;
            set
            {
                worker = value;
                if (worker.IsAdmin)
                    AdminPageChange.Visibility = Visibility.Visible;
                else
                    AdminPageChange.Visibility = Visibility.Collapsed;
            }
        }

        public MainPage()
        {
            InitializeComponent();
            workerPage = new WorkerPage();
            workerPage.Title = "Worker Page";
            adminPage = new AdminPage();
            adminPage.Title = "Admin Page";
            settingsPage = new SettingsPage();
            settingsPage.Title = "Settings Page";
            settingsPage.adminPage = adminPage;
            settingsPage.workerPage = workerPage;

            ///TODO: Replace with function call
            ContentFrame.Content = workerPage;
            TitleLabel.Content = workerPage.Title;
            ChangeActiveButton(WorkerPageChange);
        }

        private void AdminPageChange_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.IsAdmin)
            {
                if (CloseCurrentPage())
                    return;
                ContentFrame.Content = adminPage;
                TitleLabel.Content = adminPage.Title;
                ChangeActiveButton(AdminPageChange);
            }
            else
                ContentFrame.Content = workerPage;
        }

        private void WorkerPageChange_Click(object sender, RoutedEventArgs e)
        {
            if (CloseCurrentPage())
                return;
            ContentFrame.Content = workerPage;
            TitleLabel.Content = workerPage.Title;
            ChangeActiveButton(WorkerPageChange);
        }

        private void ChangeActiveButton(Button buttonOn)
        {
            Brush brushOn = Brushes.Gray;
            Brush brushOff = Brushes.LightGray;
            AdminPageChange.Background = brushOff;
            WorkerPageChange.Background = brushOff;
            SettingsPageChange.Background = brushOff;
            buttonOn.Background = brushOn;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Log out?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CloseCurrentPage())
                    return;
                worker = null;
                MainWindow.ChangeToLogInPage();
            }
        }

        private void SettingsPageChange_Click(object sender, RoutedEventArgs e)
        {
            // Change to settings page
            if (CloseCurrentPage())
                return;
            ContentFrame.Content = settingsPage;
            TitleLabel.Content = settingsPage.Title;
            ChangeActiveButton(SettingsPageChange);
        }

        private bool CloseCurrentPage()
        {
            if (((IPageClosingHandler)ContentFrame.Content).Close())
            {
                ((IPageClosingHandler)ContentFrame.Content).ClearAllFields();
                return false;
            }
            return true;
        }
    }
}
