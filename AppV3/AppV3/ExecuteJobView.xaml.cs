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

        // The ButtonExecuteJob_Click method is called when the user click the button to execute a job
        // The user can select only one or multiple jobs
        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
                // If the user didn't select anything, we send an error in the corresponding language
                if (executeJobDataGrid.SelectedItem == null)
                {
                    MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
                }
                else // If he select at least on item
                {
                    // To know how many jobs has been selected by the user we count the selected cells number and divide by the corredsponding selected columns number
                    // We also initialize a List of JobModel Object
                    int numberOfJobs = (int)(executeJobDataGrid.SelectedCells.Count / executeJobDataGrid.Columns.Count);
                    List<JobModel> jobList = new List<JobModel>() { };

                    // For each selected Job in the grid we add the corresponding JobModel to the list we initialize previously
                    foreach (JobModel obj in executeJobDataGrid.SelectedItems)
                        {
                            jobList.Add(obj);
                        }

                        // Then for Each Job in the List we use the Parrallel.ForEach() method to create a thread for each iteration, which will call the ExecuteBackup() method
                        Parallel.ForEach(jobList, job =>
                        {
                            // Method used to start job, copy the file, and also call the method that create or fill in the Log and StatusLog
                            executeJobVM.ExecuteBackup(job.jobName, job.jobType, job.sourcePath, job.targetPath);
                        });
                }
        }

        // The ButtonMainMenu_Click method is called when the user click the button to go back to the main menu
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