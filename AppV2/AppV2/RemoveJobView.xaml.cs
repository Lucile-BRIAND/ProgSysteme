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

using Newtonsoft.Json;

namespace AppV2
{
    /// <summary>
    /// Logique d'interaction pour RemoveJobView.xaml
    /// </summary>
    public partial class RemoveJobView : Window
    {
        public string file = "Jobfile.json";
        public List<JobModel> dataList = new List<JobModel>();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        RemoveJobVM removeJobVM = new RemoveJobVM();
        public RemoveJobView()
        {
            InitializeComponent();
            removeJobDataGrid.ItemsSource = MainVM.DisplayJobs();

            this.DataContext = removeJobVM.getValues();

        }

        public void ButtonRemoveBackup_Click(object sender, RoutedEventArgs e)
        {
            if (removeJobDataGrid.SelectedItem == null)
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorGrid);
            }else
            {
                JobModel jobToDelete = new JobModel();
                foreach (var obj in removeJobDataGrid.SelectedItems)
                {
                    jobToDelete = obj as JobModel;
                    RemoveJobVM.RemoveJob(jobToDelete.jobName);

                }

                removeJobDataGrid.ItemsSource = MainVM.DisplayJobs();
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
    }
}
