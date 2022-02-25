using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AppV3.Models;
using AppV3.VM;

namespace AppV3
{
    public partial class ExecuteJobView : Window
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        JobProgressPercentage jobProgressPercentageInstance = JobProgressPercentage.GetInstance;
        ExecuteJobVM executeJobVM = new ExecuteJobVM();
        MainVM mainVM = new MainVM();
        MainWindow mainWindow = new MainWindow();

        int[] PIDPauseTab = new int[100];

        public ExecuteJobView()
        {
            InitializeComponent();
            DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = mainVM.DisplayJobs();
        }

        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            SocketManager socketManager = SocketManager.GetInstance;
            Socket con = socketManager.socket;
            string value = socketManager.ListenToNetwork(con);
            var valueModify = Regex.Match(value, @"^([\w\-]+)");
            string indexString = Regex.Match(value, @"\d+").Value;
            MessageBox.Show(valueModify.ToString());
            switch (valueModify.ToString())
            {
                case "Stop":
                    MessageBox.Show("STOP");
                    int index = Int32.Parse(indexString);
                    MessageBox.Show(index.ToString());
                    Process processStopStart = new Process();
                    processStopStart.StartInfo.FileName = "notepad";
                    processStopStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    processStopStart.Start();
                    Trace.WriteLine(processStopStart.Id);
                    executeJobVM.InitJobStopName(processStopStart.Id, index);
                    break;
                case "Pause":
                    MessageBox.Show("PAUSE");
                    int index2 = Int32.Parse(indexString);
                    Process processStart = new Process();
                    processStart.StartInfo.FileName = "notepad";
                    processStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    processStart.Start();
                    PIDPauseTab[index2] = processStart.Id;
                    Trace.WriteLine(processStart.Id);
                    executeJobVM.InitJobPauseName(processStart.Id, index2);
                    break;
                case "Resume":
                    MessageBox.Show("RESUME");
                    int index3 = Int32.Parse(indexString);
                    Process processStop = Process.GetProcessById(PIDPauseTab[index3]);
                    Trace.WriteLine("test" + processStop);
                    try
                    {
                        processStop.Kill();
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        // The ButtonExecuteJob_Click method is called when the user click the button to execute a job
        // The user can select only one or multiple jobs
        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            StartBackup();
        }


        private async void StartBackup()
        {
            //Method that creates the threads and start them

            int index = executeJobDataGrid.SelectedIndex;
            if (executeJobDataGrid.SelectedIndex > -1)
            {
                int numberOfJobs = (int)(executeJobDataGrid.SelectedCells.Count / executeJobDataGrid.Columns.Count);
                List<JobModel> jobList = new List<JobModel>() { };

                if (executeJobDataGrid.SelectedItem == null)
                {
                    MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
                }
                else
                {
                    foreach (JobModel obj in executeJobDataGrid.SelectedItems)
                    {
                        jobList.Add(obj);
                    }
                    await Task.Factory.StartNew(() =>
                    Parallel.ForEach(jobList, job =>
                    {
                        executeJobVM.ExecuteBackup(job.jobName, job.jobType, job.sourcePath, job.targetPath, index);
                    }));
                }
            }

        }

        // The ButtonMainMenu_Click method is called when the user click the button to go back to the main menu
        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            //Stop the selected thread

            int index = executeJobDataGrid.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            else
            {
                Process processStopStart = new Process();
                processStopStart.StartInfo.FileName = "notepad";
                processStopStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStopStart.Start();
                Trace.WriteLine(processStopStart.Id);
                executeJobVM.InitJobStopName(processStopStart.Id, index);

            }
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            //Pause the selected thread

            int index = executeJobDataGrid.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            else
            {
                Process processStart = new Process();
                processStart.StartInfo.FileName = "notepad";
                processStart.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStart.Start();
                PIDPauseTab[index] = processStart.Id;
                Trace.WriteLine(processStart.Id);
                executeJobVM.InitJobPauseName(processStart.Id, index);

            }
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            //Resume the selected thread

            int index = executeJobDataGrid.SelectedIndex;

            if (index < 0)
            {
                return;
            }
            else
            {
                Process processStop = Process.GetProcessById(PIDPauseTab[index]);
                Trace.WriteLine("test" + processStop);
                try
                {
                    processStop.Kill();
                }
                catch
                {

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Activate the background worker 

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
        }


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            for (int i = 1; i <= 100; i++)
            {
                string percentage = jobProgressPercentageInstance.percentage.ToString();
                worker.ReportProgress(Int32.Parse(percentage));
                Thread.Sleep(500);
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    break;
                }
            }
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Trace.WriteLine(e.ProgressPercentage);
            executeJobProgressBar.Value = e.ProgressPercentage;
            executeJobProgressTextBlock.Text = e.ProgressPercentage.ToString();
        }




















    }
}