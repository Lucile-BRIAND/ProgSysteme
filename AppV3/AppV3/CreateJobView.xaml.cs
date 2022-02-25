using System;
using System.Windows;
using System.Windows.Controls;
using AppV3.VM;
using AppV3.Models;
using WinForms = System.Windows.Forms;

namespace AppV3
{
    public partial class CreateJobView : Window
    {
        // Initialisation of  Language instance, in order to have access at choosen language in this view
        public LanguageFile singletonLang = LanguageFile.GetInstance;
        // Initialisation of the used objects
        public CreateJobVM createJobVM = new CreateJobVM();
        public MainWindow mainWindow = new MainWindow();

        // The CreateJobView constructor is used to initialise the components and DataBinding 
        public CreateJobView()
        {
            InitializeComponent();

            DataContext = createJobVM.getValues();
        }

        // The DataGrid_SelectionChanged method need to be declare, even if nothing happens  in it
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //The ButtonMainMenu_Click method is called when the user click the button to go back to the main menu 
        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }
        // The DataGrid_SelectionChanged method need to be declare, even if nothing happens  in it
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // The ButtonCreateBackup_Click method is enabling to validate the creation of a job
        private void ButtonCreateBackup_Click(object sender, RoutedEventArgs e)
        {
            // Conditon checking if all requirements are completes 
            if (jobNameTextBox.Text == "" | jobTypeComboBox.Text == "" | sourcePathTextBox.Text == "" | targetPathTextBox.Text == "")
            {
                // Display error message
                MessageBox.Show(singletonLang.ReadFile().ErrorExecute);
            }
            else
            {
                // Path formating allowing to have correct format used by the SaveJob method
                string sourcePath = sourcePathTextBox.Text.Replace("\\", "/");
                string targetPath = targetPathTextBox.Text.Replace("\\", "/");
                // MessageBox.Show(jobNameTextBox.Text);
                // The SaveJob method is called to save the created job and write it in the jobfile
                createJobVM.SaveJob(jobNameTextBox.Text, jobTypeComboBox.Text, sourcePath, targetPath);
                // Display of main window
                mainWindow.Show();
                // Closing of the createJobView
                Close();
            }

        }
        // The SourceOpenFolderDialog_Click used to open a window allowing to the user to choose the source path of the job
        private void SourceOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            // Creation of the open folder dialog
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            // Open the selected folder window
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                // If the DialogResult work well, we display the source path
                string sPath = folderDialog.SelectedPath;
                sourcePathTextBox.Text = sPath;
            }
        }
        // The TargetOpenFolderDialog_Click used to open a window allowing to the user to choose the target path of the job
        private void TargetOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            // Creation of the open folder dialog
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            // Open the selected folder window
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                // If the DialogResult work well, we display the source path
                string sPath = folderDialog.SelectedPath;
                targetPathTextBox.Text = sPath;
            }
        }
    }
}