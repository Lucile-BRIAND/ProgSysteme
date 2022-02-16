﻿using System;
using System.Collections.Generic;
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
        public delegate void ThreadDelegate(object jobToExecuteParameter);
        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
        }

        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            if (executeJobDataGrid.SelectedItem == null)
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
            }
            else
            {
                ThreadDelegate del1 = (jobToExecuteParameter) =>
                {
                    JobModel jobToExecute = (JobModel)jobToExecuteParameter;
                    executeJobVM.ExecuteBackup(jobToExecute.jobName, jobToExecute.jobType, jobToExecute.sourcePath, jobToExecute.targetPath, JobSoftwareNameTextBox.Text, logFileFormat);
                };
                JobModel selectedJobToExecute = new JobModel();
                foreach (var obj in executeJobDataGrid.SelectedItems)
                {
                    if (executeJobDataGrid.SelectedIndex > -1)
                    {
                        selectedJobToExecute = obj as JobModel;
                        Thread thread1 = new Thread(new ParameterizedThreadStart(del1.Invoke));
                        thread1.Start(selectedJobToExecute);
                        //executeJobVM.ExecuteBackup(jobToExecute.jobName, jobToExecute.jobType, jobToExecute.sourcePath, jobToExecute.targetPath, JobSoftwareNameTextBox.Text, logFileFormat);
                    }
                }

            }
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
            else if(LogFileFormatComboBox.SelectedIndex == 1)
            {
                logFileFormat = "xml";
            }
        }
    }
}