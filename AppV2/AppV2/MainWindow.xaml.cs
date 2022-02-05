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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppV2.VM;
using AppV2.Models;

namespace AppV2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCreateJobView_Click(object sender, RoutedEventArgs e)
        {
            CreateJobView createJobView = new CreateJobView();
            createJobView.Show();
            Close();
        }

        private void ButtonExecuteJobView_Click(object sender, RoutedEventArgs e)
        {
            ExecuteJobView executeJob = new ExecuteJobView();
            executeJob.Show();
            Close();
        }

        private void ButtonRemoveJobView_Click(object sender, RoutedEventArgs e)
        {
            RemoveJobView remove = new RemoveJobView();
            remove.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(selectedLanguageComboBox.SelectedIndex.ToString());
            ChooseLanguageVM chooseLanguageVM = new ChooseLanguageVM();
            LanguageFile cmp= chooseLanguageVM.SaveLanguage(selectedLanguageComboBox.SelectedIndex);
            createBackupButton.Content = cmp.MainCreate;
            executeBackupButton.Content = cmp.MainExecute;
            removeBackupButton.Content = cmp.MainRemove;
            chooseActionTextBlock.Text = cmp.Main;
            jobSoftwareLabel.Content = cmp.JobSoftware;
            validationJobSoftwareButton.Content = cmp.ValidationJobSoftware;


        }
    }
}
