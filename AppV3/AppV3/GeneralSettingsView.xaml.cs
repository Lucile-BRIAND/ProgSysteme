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
using AppV3.VM;
using AppV3.Models;
using AppV3;
using System.Diagnostics;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour GeneralSettingsView.xaml
    /// </summary>
    public partial class GeneralSettingsView : Window
    {
        public string logFileFormat;
        MainVM mainVM = new MainVM();
        ExecuteJobVM executeJobVM = new ExecuteJobVM();
        LanguageFile singletonLang = LanguageFile.GetInstance;
        public GeneralSettingsView()
        {
            InitializeComponent();
            this.DataContext = mainVM.getValues();
        }

        private void LogFileFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LogFileFormatComboBox.SelectedIndex == 0)
            {
                logFileFormat = "json";
                executeJobVM.InitFormat(logFileFormat);
            }
            else if (LogFileFormatComboBox.SelectedIndex == 1)
            {
                logFileFormat = "xml";
                executeJobVM.InitFormat(logFileFormat);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(selectedLanguageComboBox.SelectedIndex.ToString());
            ChooseLanguageVM chooseLanguageVM = new ChooseLanguageVM();
            chooseLanguageVM.SaveLanguage(selectedLanguageComboBox.SelectedIndex);
            this.DataContext = mainVM.getValues();
        }

        private void ButtonSelectJobSoftware_Click(object sender, RoutedEventArgs e)
        {
            if (jobSoftawreNameTextBox.Text == "")
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorExecute);
            }
            else
            {
                executeJobVM.InitJobSoftwareName(jobSoftawreNameTextBox.Text);
            }
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        public void ExtentionToPrioritize_Checked(object sender, RoutedEventArgs e)
        {
            executeJobVM.GetExtensionToPrioritize(((bool)PriorityTXTCheckbox.IsChecked, (bool)PriorityPDFCheckbox.IsChecked, (bool)PriorityJPGCheckbox.IsChecked, (bool)PriorityPNGCheckBox.IsChecked));
        }
        private void ExtentionToEncrypit_Checked(object sender, RoutedEventArgs e)
        {
            executeJobVM.GetFileExtentions((bool)CheckboxTXT.IsChecked, (bool)CheckboxPDF.IsChecked, (bool)CheckboxJPG.IsChecked, (bool)CheckboxPNG.IsChecked);
        }


        private void MaximumFileSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileSize fileSize = FileSize.GetInstance;
            ComboBoxItem size = maximumFileSizeComboBox.SelectedItem as ComboBoxItem;
            Trace.WriteLine("SIZE :" + size.Content);
            fileSize.FileMaxSize = Convert.ToInt32(size.Content);
        }
    }
}
