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
using AppV3.VM;
using AppV3.Models;
using AppV3;
using System.Threading;
using System.Diagnostics;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        MainVM mainVM = new MainVM();
        
        
        public MainWindow()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) {
                MessageBox.Show("Application was already running. Only one instance of this application is allowed");
                Close();
                return; 
            }

            InitializeComponent();
            this.DataContext = mainVM.getValues();
        }

        private void ButtonCreateJobView_Click(object sender, RoutedEventArgs e)
        {
            CreateJobView createJobView = new CreateJobView();
            createJobView.Show();
            Close();
        }

        private void ButtonExecuteJobView_Click(object sender, RoutedEventArgs e)
        {
            ExecuteJobView executeJob = new ExecuteJobView();
            executeJob.Show();
            Close();
        }

        private void ButtonRemoveJobView_Click(object sender, RoutedEventArgs e)
        {
            RemoveJobView remove = new RemoveJobView();
            remove.Show();
            Close();
        }



        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            GeneralSettingsView generalSettingsView = new GeneralSettingsView();
            generalSettingsView.Show();
            Close();
        }
    }
}
