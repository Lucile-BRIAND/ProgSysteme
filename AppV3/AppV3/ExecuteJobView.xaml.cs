﻿using System;
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
        private string logFileFormat;
        //JobModel jobToExecute;
        Thread[] Threads;

        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
        }

        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            int numberOfJobs = (int)(executeJobDataGrid.SelectedCells.Count / executeJobDataGrid.Columns.Count);
            Trace.WriteLine("numberOfJobs : " + numberOfJobs);
            Threads = new Thread[numberOfJobs];
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
                        myThread.Name = "thread" + counter.ToString();
                        myThread.IsBackground = true;
                        Trace.WriteLine("numberOfJobs (IF) : " + numberOfJobs);
                        if (counter < numberOfJobs)
                        {
                            Threads[counter] = myThread;
                            Threads[counter].Start(obj);
                            //ThreadPool.QueueUserWorkItem(delegate
                            //{
                            //    Threads[counter].Start(obj);
                            //});
                        }
                        
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
                executeJobVM.ExecuteBackup(jobToExecute.jobName, jobToExecute.jobType, jobToExecute.sourcePath, jobToExecute.targetPath);
            });

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        //private void ExecuteAllBackupButton_Click(object sender, RoutedEventArgs e)
        //{
        //    executeJobVM.ExecutAllBackup(JobSoftwareNameTextBox.Text, logFileFormat);
        //}

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "explorer";
            process.Start();
            executeJobVM.InitJobSoftwareName("explorer");
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < executeJobDataGrid.SelectedItems.Count; i++)
            {
                Threads[i].Abort();
                MessageBox.Show(Threads[i].Name);
            }

        }
    }
}
