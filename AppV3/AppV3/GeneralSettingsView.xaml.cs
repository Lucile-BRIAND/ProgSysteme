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
        // The LogFileFormatComboBox_SelectionChanged method is called when the user chose a log format either JSON or XML in the related ComboBox
        private void LogFileFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                 
            // If the user select the first ComboBox Item : corresponding to JSON
            if (LogFileFormatComboBox.SelectedIndex == 0)
            {
                logFileFormat = "json";
                executeJobVM.InitFormat(logFileFormat);
            }
            // Else if the user select the second ComboBox Item : corresponding to XML
            else if (LogFileFormatComboBox.SelectedIndex == 1)
            {
                logFileFormat = "xml";
                executeJobVM.InitFormat(logFileFormat);
            }
        }
        // The ComboBox_SelectionChanged method is called when the user chose a language either English or French in the related ComboBox
        private void ComboBoxChooseLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Here we instanciate a new object from ChooseLanguageVM class, responsible for the language selection save
            ChooseLanguageVM chooseLanguageVM = new ChooseLanguageVM();
            chooseLanguageVM.SaveLanguage(selectedLanguageComboBox.SelectedIndex);

            // After the language has been save : we display the corresponding language in the view, using Binding
            this.DataContext = mainVM.getValues();
        }
        // The ButtonSelectJobSoftware_Click method is called when the user has click the ButtonSelectJobSoftware
        private void ButtonSelectJobSoftware_Click(object sender, RoutedEventArgs e)
        {
            // If the jobSoftawreNameTextBox is empty, meaning the user did not fill in the TextBox to specify a JobSoftware that will "lock" the execution of jobs
            if (jobSoftawreNameTextBox.Text == "")
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorExecute);
            }
            // Else the jobSoftawreNameTextBox is fill in, meaning the user did fill in the TextBox to specify a JobSoftware that will "lock" the execution of jobs
            else
            {
                executeJobVM.InitJobSoftwareName(jobSoftawreNameTextBox.Text);
            }
        }
        // The ButtonMainMenu_Click method is called when the user click the button to go back to the main menu
        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        // The CheckBox_Checked method is called when the user click one of the 4 checkbox, for each checkbox we save the corresponding value : true for checked, false for unchecked
        // This checkbox permit us to recover the extension to encrypt related to the user's demand
        private void ComboBoxExtensionToEncrypt_Checked(object sender, RoutedEventArgs e)
        {
            executeJobVM.GetExtentionsToEncrypt((bool)CheckboxTXT.IsChecked, (bool)CheckboxPDF.IsChecked, (bool)CheckboxJPG.IsChecked, (bool)CheckboxPNG.IsChecked);
        }
        // The MaximumFileSizeComboBox_SelectionChanged method is called when the user select a value in the ComboBox, for each value we save the corresponding Maximum File Size for simultaneous transferts
        private void ComboBoxMaximumFileSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileSize fileSize = FileSize.GetInstance;
            ComboBoxItem size = maximumFileSizeComboBox.SelectedItem as ComboBoxItem;
            Trace.WriteLine("SIZE :" + size.Content);
            fileSize.FileMaxSize = Convert.ToInt32(size.Content) * 1000;
        }
        private void CheckBoxExtentionsToPrioritize_Checked(object sender, RoutedEventArgs e)
        {
            executeJobVM.GetExtentionsToPrioritize((bool)Priority_TXTCheckbox.IsChecked, (bool)Priority_PDFCheckbox.IsChecked, (bool)Priority_JPGCheckbox.IsChecked, (bool)Priority_PNGCheckBox.IsChecked);
        }
    }
}
