using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using AppV3.Models;
using AppV3.VM;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour ExecuteJobView.xaml
    /// </summary>
    public partial class ExecuteJobView : Window
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        ExecuteJobVM executeJobVM = new ExecuteJobVM();
        MainVM mainVM = new MainVM();
        MainWindow mainWindow = new MainWindow();
        private string logFileFormat;
        //JobModel jobToExecute;
        Thread[] Threads;
        private static ManualResetEvent mre = new ManualResetEvent(false);
        private Socket server;

        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(localEndPoint);
            server.Listen(10);
        }

        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            Threads = new Thread[executeJobDataGrid.SelectedItems.Count];
            int counter = 0;
            Thread myThread;

            if (executeJobDataGrid.SelectedItem == null)
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
            }
            else
            {
                foreach (JobModel obj in executeJobDataGrid.SelectedItems)
                {
                    if (executeJobDataGrid.SelectedIndex > -1)
                    {
                        myThread = new Thread(new ParameterizedThreadStart(StartBackup));
                        myThread.Name = "thread" + executeJobDataGrid.SelectedIndex.ToString();
                        Threads[counter] = myThread;
                        Threads[counter].Start(obj);

                    }
                    counter++;
                    Thread.Sleep(1000);
                }

            }
        }
        private void StartBackup(object obj)
        {
            JobModel jobToExecute = (JobModel)obj;
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                executeJobVM.ExecuteBackup(server, jobToExecute.jobName, jobToExecute.jobType, jobToExecute.sourcePath, jobToExecute.targetPath, JobSoftwareNameTextBox.Text, logFileFormat);
            });

        }

        private void ButtonStopJob_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                mre.WaitOne();
                //Do work.
            }
        }

        private void ButtonPauseJob_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void ExecuteAllBackupButton_Click(object sender, RoutedEventArgs e)
        {
            executeJobVM.ExecutAllBackup(JobSoftwareNameTextBox.Text, logFileFormat);
        }

        private void LogFileFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (LogFileFormatComboBox.SelectedIndex == 0)
            {
                logFileFormat = "json";
            }
            else if (LogFileFormatComboBox.SelectedIndex == 1)
            {
                logFileFormat = "xml";
            }
        }
    }
}
