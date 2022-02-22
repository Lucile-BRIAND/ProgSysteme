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
using System.Net.Sockets;

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
            // If the EasySaveAppV3 is already running we lock the launch of any other instance of the application
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) {
                MessageBox.Show("Application was already running. Only one instance of this application is allowed");
                Close();
                return; 
            }
            InitializeComponent();
            this.DataContext = mainVM.getValues();
        }
        // The ButtonCreateJobView_Click method is called when the user click the button to go display CreateJob View
        private void ButtonCreateJobView_Click(object sender, RoutedEventArgs e)
        {
            CreateJobView createJobView = new CreateJobView();
            createJobView.Show();
            Close();
        }
        // The ButtonExecuteJobView_Click method is called when the user click the button to go display ExecuteJob View
        private void ButtonExecuteJobView_Click(object sender, RoutedEventArgs e)
        {
            ExecuteJobView executeJob = new ExecuteJobView();
            executeJob.Show();
            Close();
        }
        // The ButtonRemoveJobView_Click method is called when the user click the button to go display RemoveJob View
        private void ButtonRemoveJobView_Click(object sender, RoutedEventArgs e)
        {
            RemoveJobView remove = new RemoveJobView();
            remove.Show();
            Close();
        }
        // The ButtonSettings_Click method is called when the user click the button to go display GeneralSetting View
        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            GeneralSettingsView generalSettingsView = new GeneralSettingsView();
            generalSettingsView.Show();
            Close();
        }
        private void startClientConnection_Click(object sender, RoutedEventArgs e)
        {
            SocketManager socketManage = SocketManager.GetInstance;
            Socket serveur = socketManage.Connect();
            Socket client = socketManage.AcceptConnection(serveur);
            MessageBox.Show("Connexion reussie");
            socketManage.socket = client;
        }
    }
}
