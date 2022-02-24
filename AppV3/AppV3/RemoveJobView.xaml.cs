using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AppV3.Models;
using AppV3.VM;

namespace AppV3
{
    public partial class RemoveJobView : Window
    {
        
        public List<JobModel> dataList = new List<JobModel>();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        RemoveJobVM removeJobVM = new RemoveJobVM();
        MainWindow mainWindow = new MainWindow();
        MainVM mainVM = new MainVM();
        public RemoveJobView()
        {
            InitializeComponent();
            removeJobDataGrid.ItemsSource = mainVM.DisplayJobs();
            DataContext = removeJobVM.getValues();
        }

        // The ButtonRemoveBackup_Click method is called when the user click the button to delete an existing job
        public void ButtonRemoveBackup_Click(object sender, RoutedEventArgs e)
        {
            // If the user did not select any item in the grid, we display an error in the corresponding language
            if (removeJobDataGrid.SelectedItem == null)
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
            }
            // If the user did select any item in the grid, we delete the corresponding job(s)
            else
            {
                JobModel jobToDelete = new JobModel();
                foreach (var obj in removeJobDataGrid.SelectedItems)
                {
                    jobToDelete = obj as JobModel;
                    RemoveJobVM.RemoveJob(jobToDelete.jobName);

                }
                // Then we display the existing job(s), showing that the deleted job(s) is / are no longer in the grid
                removeJobDataGrid.ItemsSource = mainVM.DisplayJobs();
            }
        }

        // The DataGrid_SelectionChanged method need to be declare, even if nothing happens in it
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // The ButtonMainMenu_Click method is called when the user click the button to go back to the main menu
        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }
    }
}
