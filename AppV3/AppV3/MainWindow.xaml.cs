using System;
using System.Windows;
using AppV3.VM;
using AppV3.Models;
using System.Diagnostics;
using System.Net.Sockets;

namespace AppV3
{
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
            DataContext = mainVM.getValues();
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

        //This button allows the main application to accept connections from client applications
        private void startClientConnection_Click(object sender, RoutedEventArgs e)
        {
            SocketManager socketManage = SocketManager.GetInstance;
            SocketManagerBackupsName socketManageBackupsName = SocketManagerBackupsName.GetInstance;
            Socket serveur = socketManage.Connect();
            Socket client = socketManage.AcceptConnection(serveur);
            Socket serveur2 = socketManageBackupsName.Connect();
            Socket client2 = socketManageBackupsName.AcceptConnection(serveur2);
            MessageBox.Show("Connexion reussie");
            socketManage.socket = client;
            socketManageBackupsName.socket = client2;
        }
    }
}
