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

//using WPF_Teller_App.Data.Models;
using Bank_Db_Class_Library;
using WPF_Teller_App.Pages;

namespace WPF_Teller_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogInPage logInPage;
        MainPage mainPage;

        public MainWindow()
        {
            InitializeComponent();

            logInPage = new LogInPage();
            mainPage = new MainPage();

            logInPage.MainWindow = this;
            logInPage.MainPage = mainPage;

            //MainFrame.Source = new Uri("Pages/LogInPage.xaml");
            ChangePage(logInPage, LogInPage.PageWidth, LogInPage.PageHeigth);
            this.ResizeMode = ResizeMode.NoResize;
        }

        public void ChangePage(Page page, int width, int height)
        {
            MainFrame.Content = page;
            this.Width = width;
            this.Height = height;
        }
    }
}
