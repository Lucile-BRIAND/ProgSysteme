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
using AppV3.VM;
using AppV3.Models;
using System.Net;
using System.Net.Sockets;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        MainVM mainVM = new MainVM();
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainVM.getValues();
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


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(selectedLanguageComboBox.SelectedIndex.ToString());
            ChooseLanguageVM chooseLanguageVM = new ChooseLanguageVM();
            chooseLanguageVM.SaveLanguage(selectedLanguageComboBox.SelectedIndex);
            this.DataContext = mainVM.getValues();

        }
    }
}
