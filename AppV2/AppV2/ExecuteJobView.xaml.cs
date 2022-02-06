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
using AppV2.Models;
using AppV2.VM;

namespace AppV2
{
    /// <summary>
    /// Logique d'interaction pour ExecuteJobView.xaml
    /// </summary>
    public partial class ExecuteJobView : Window
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        ExecuteJobVM executeJobVM = new ExecuteJobVM();
        MainVM mainVM = new MainVM();
        public ExecuteJobView()
        {
            InitializeComponent();
            this.DataContext = executeJobVM.getValues();
            executeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
        }

        private void ButtonExecuteJob_Click(object sender, RoutedEventArgs e)
        {
            JobModel jobToExecute = new JobModel();
            foreach (var obj in executeJobDataGrid.SelectedItems)
            {
                jobToExecute = obj as JobModel;
                executeJobVM.ExecuteBackup(jobToExecute.jobName, jobToExecute.jobType, jobToExecute.sourcePath, jobToExecute.targetPath, JobSoftwareNameTextBox.Text);
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void ExecuteAllBackupButton_Click(object sender, RoutedEventArgs e)
        {
            executeJobVM.ExecutAllBackup(JobSoftwareNameTextBox.Text);
        }

        private void ValidationJobSoftwareNameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
