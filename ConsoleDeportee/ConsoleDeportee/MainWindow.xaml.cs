using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Socket cokk;
        public MainWindow()
        {
            
            InitializeComponent();
            ConnetionToEasySave connexionToSave = new ConnetionToEasySave();
            MessageBox.Show("Tentative de connexion");
            cokk = connexionToSave.Init();
            


            //executeJobDataGrid.ItemsSource = ConnetionToEasySave.EcouterReseau(connection);
            // Deconnecter(connection);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonPauseJob_Click(object sender, RoutedEventArgs e)
        {

             
           
        }

        private void ButtonStopJob_Click(object sender, RoutedEventArgs e)
        {

            executeJobDataGrid.ItemsSource = ConnetionToEasySave.EcouterReseau(cokk); 
        }

        private void pbstatus1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Thread listener = new Thread(new ThreadStart(test));
            //listener.IsBackground = true;
            //listener.Start();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();



        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {

                (sender as BackgroundWorker).ReportProgress(i);

                Thread.Sleep(2050);

            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //initialisation de la barre de progression avec le pourcentage de progression
            string value = ConnetionToEasySave.EcouterReseau(cokk);
            
            //pbstatus1.Value = e.ProgressPercentage;
            pbstatus1.Value = Int64.Parse(value);
            //Affichage de la progression sur un label
            lb_etat_prog_server.Content = value;
            
            if(value == "100")
            {
                return;
            }


        }
        public void test()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                while (true)
                {

                    string value = ConnetionToEasySave.EcouterReseau(cokk);
                    //pbstatus1.Value = Int64.Parse(value);
                    if (value == "100")
                    {
                        cokk.Close();

                    }
                }
            });

        }
    }
}
