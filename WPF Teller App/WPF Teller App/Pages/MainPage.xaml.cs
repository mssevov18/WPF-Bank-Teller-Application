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
        public static int PageHeigth => 450;

        WorkerPage workerPage;
        AdminPage  adminPage;

        private BankWorker worker;
        public BankWorker  Worker {
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
            adminPage = new AdminPage();

            ContentFrame.Content = workerPage;
            ChangeActiveButton(WorkerPageChange, AdminPageChange);
        }

        private void AdminPageChange_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.IsAdmin)
            {
                ContentFrame.Content = adminPage;
                ChangeActiveButton(AdminPageChange, WorkerPageChange);
            }
            else
                ContentFrame.Content = workerPage;
        }

        private void WorkerPageChange_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = workerPage;
            ChangeActiveButton(WorkerPageChange, AdminPageChange);
        }

        private void ChangeActiveButton(Button buttonOn, Button buttonOff)
        {
            Brush brushOn = Brushes.Gray;
            Brush brushOff = Brushes.LightGray;
            buttonOff.Background = brushOff;
            buttonOn.Background = brushOn;
        }
    }
} 
