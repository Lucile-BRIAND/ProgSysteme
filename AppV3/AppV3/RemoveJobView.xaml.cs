using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using Newtonsoft.Json;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour RemoveJobView.xaml
    /// </summary>
    public partial class RemoveJobView : Window
    {
        
        public List<JobModel> dataList = new List<JobModel>();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        RemoveJobVM removeJobVM = new RemoveJobVM();
        MainWindow mainWindow = new MainWindow();
        public RemoveJobView()
        {
            InitializeComponent();
            removeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
            this.DataContext = removeJobVM.getValues();
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
                removeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
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
