using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ConsoleDeportee.VM;

namespace ConsoleDeportee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket conPourcentage;
        Socket conNomSauvegarde;
        BackgroundWorker worker = new BackgroundWorker();
        public MainWindow()
        {

            InitializeComponent();
            ConnetionToEasySave connexionToSave = new ConnetionToEasySave();
            MessageBox.Show("Tentative de connexion");
            conPourcentage = connexionToSave.InitPourcentage();
            conNomSauvegarde = connexionToSave.InitNomSauvegarde();
           

            //executeJobDataGrid.ItemsSource = ConnetionToEasySave.EcouterReseau(connection);
            // Deconnecter(connection);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ButtonPauseJob_Click(object sender, RoutedEventArgs e)
        {
            Byte[] buffer = new Byte[1024];
            Byte[] buffer2 = new byte[1024];
            string content = (string)nameBackups.Content;
            buffer = Encoding.UTF8.GetBytes("Pause " + content);
            conPourcentage.Send(buffer);
        }
        private void ButtonResumeJob_Click(object sender, RoutedEventArgs e)
        {
            Byte[] buffer = new Byte[1024];
            Byte[] buffer2 = new byte[1024];
            string content = (string)nameBackups.Content;
            buffer = Encoding.UTF8.GetBytes("Resume " + content);         
            conPourcentage.Send(buffer);
        }

        private void ButtonStopJob_Click(object sender, RoutedEventArgs e)
        {
            Byte[] buffer = new Byte[1024];
            Byte[] buffer2 = new byte[1024];
            string content = (string)nameBackups.Content;
            buffer = Encoding.UTF8.GetBytes("Stop " + content);
            conPourcentage.Send(buffer);
           
        }

        private void pbstatus1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker workerPercentage = new BackgroundWorker();
            workerPercentage.WorkerReportsProgress = true;
            workerPercentage.WorkerSupportsCancellation = true;
            workerPercentage.ProgressChanged += workerPercentage_ProgressChanged;
            workerPercentage.DoWork += workerPercentage_DoWork;
            workerPercentage.RunWorkerCompleted += workerPercentage_RunWorkerCompleted;
            workerPercentage.RunWorkerAsync();
            BackgroundWorker workerNameSave = new BackgroundWorker();
            workerNameSave.WorkerReportsProgress = true;
            workerNameSave.WorkerSupportsCancellation = true;
            workerNameSave.ProgressChanged += workerNameSave_ProgressChanged;
            workerNameSave.DoWork += workerNameSave_DoWork;
            workerNameSave.RunWorkerCompleted += workerNameSave_RunWorkerCompleted;
            workerNameSave.RunWorkerAsync();

        }
        void workerPercentage_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            for (int i = 1; i <= 100; i++)
            {
                string value = ConnetionToEasySave.EcouterReseau(conPourcentage);
                worker.ReportProgress(Int32.Parse(value));
                Thread.Sleep(2200);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    break;
                }
            }
        }
        void workerNameSave_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            for (int i = 1; i <= 100; i++)
            {      
                string value = ConnetionToEasySave.EcouterReseauBackupsName(conNomSauvegarde);                
                worker.ReportProgress(i, value);
                Thread.Sleep(2200);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    break;
                }
            }
        }

        void workerPercentage_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Trace.WriteLine(e.ProgressPercentage);
            pbstatus1.Value = e.ProgressPercentage;
            lb_etat_prog_server.Content = e.ProgressPercentage;
        }


        void workerNameSave_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //string values = "";
            //foreach (string name in (List<String>)e.UserState)
            //{          
            //     values += name;
            //}
            Trace.WriteLine((String)e.UserState);
            nameBackups.Content = (String)e.UserState;
        }


        private void workerPercentage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("It's done !");
        }
        private void workerNameSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("It's done !");
        }

        public void test()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                while (true)
                {

                    string value = ConnetionToEasySave.EcouterReseau(conPourcentage);
                    //pbstatus1.Value = Int64.Parse(value);
                    if (value == "100")
                    {
                        conPourcentage.Close();

                    }
                }
            });

        }

   
    }
}
