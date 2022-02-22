using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        Thread[] Threads;
        int[] PIDPauseTab = new int[100];

        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
        }

        // The ButtonExecuteJob_Click method is called when the user click the button to execute a job
        // The user can select only one or multiple jobs
        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            StartBackup();
        }

        private async void StartBackup()
        {
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
    }
}