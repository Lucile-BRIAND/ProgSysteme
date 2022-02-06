using AppV2.VM;
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
using AppV2.VM;
using AppV2.Models;

namespace AppV2
{
    /// <summary>
    /// Logique d'interaction pour CreateJobView.xaml
    /// </summary>
    public partial class CreateJobView : Window
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        CreateJobVM createJobVM1 = new CreateJobVM();
        public CreateJobView()
        {
            InitializeComponent();

            this.DataContext = createJobVM1.getValues();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonCreateBackup_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(jobNameTextBox.Text);
            CreateJobVM createJobVM = new CreateJobVM();
            createJobVM.SaveJob(jobNameTextBox.Text, jobTypeComboBox.Text, sourcePathTextBox.Text, targetPathTextBox.Text);
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
