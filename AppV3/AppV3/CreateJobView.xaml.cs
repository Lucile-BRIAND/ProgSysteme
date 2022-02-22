using AppV3.VM;
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
using WinForms = System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AppV3
{
    /// <summary>
    /// Logique d'interaction pour CreateJobView.xaml
    /// </summary>
    public partial class CreateJobView : Window
    {
        LanguageFile singletonLang = LanguageFile.GetInstance;
        CreateJobVM createJobVM = new CreateJobVM();
        MainWindow mainWindow = new MainWindow();
        public CreateJobView()
        {
            InitializeComponent();

            this.DataContext = createJobVM.getValues();
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonCreateBackup_Click(object sender, RoutedEventArgs e)
        {
            if(jobNameTextBox.Text == "" | jobTypeComboBox.Text == "" | sourcePathTextBox.Text == "" | targetPathTextBox.Text == "")
            {
                MessageBox.Show(singletonLang.ReadFile().ErrorExecute);
            }
            else
            {
                string sourcePath = sourcePathTextBox.Text.Replace("\\", "/");
                string targetPath = targetPathTextBox.Text.Replace("\\", "/");
                // MessageBox.Show(jobNameTextBox.Text);
                createJobVM.SaveJob(jobNameTextBox.Text, jobTypeComboBox.Text, sourcePath, targetPath);
                mainWindow.Show();
                Close();
            }

        }

        private void SourceOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                //----< Selected Folder >----
                //< Selected Path >
                String sPath = folderDialog.SelectedPath;
                sourcePathTextBox.Text = sPath;
                //</ Selected Path >

                //--------< Folder >--------
                DirectoryInfo folder = new DirectoryInfo(sPath);
                if (folder.Exists)
                {
                    //------< @Loop: Files >------
                    foreach (FileInfo fileInfo in folder.GetFiles())
                    {
                        //----< File >----
                        String sDate = fileInfo.CreationTime.ToString("yyyy-MM-dd");
                        Debug.WriteLine("#Debug: File: " + fileInfo.Name + " Date:" + sDate);
                        //----</ File >----
                    }
                    //------</ @Loop: Files >------
                }
                //--------</ Folder >--------
                //----</ Selected Folder >----
            }
        }

        private void TargetOpenFolderDialog_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                //----< Selected Folder >----
                //< Selected Path >
                String sPath = folderDialog.SelectedPath;
                targetPathTextBox.Text = sPath;
                //</ Selected Path >

                //--------< Folder >--------
                DirectoryInfo folder = new DirectoryInfo(sPath);
                if (folder.Exists)
                {
                    //------< @Loop: Files >------
                    foreach (FileInfo fileInfo in folder.GetFiles())
                    {
                        //----< File >----
                        String sDate = fileInfo.CreationTime.ToString("yyyy-MM-dd");
                        Debug.WriteLine("#Debug: File: " + fileInfo.Name + " Date:" + sDate);
                        //----</ File >----
                    }
                    //------</ @Loop: Files >------
                }
                //--------</ Folder >--------
                //----</ Selected Folder >----
            }
        }
    }
}
