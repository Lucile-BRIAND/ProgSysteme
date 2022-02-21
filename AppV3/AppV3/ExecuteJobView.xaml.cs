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

        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
        }

        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
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

                    Parallel.ForEach(jobList, job =>
                    {
                        executeJobVM.ExecuteBackup(job.jobName, job.jobType, job.sourcePath, job.targetPath);
                    });
                }
            }
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {

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