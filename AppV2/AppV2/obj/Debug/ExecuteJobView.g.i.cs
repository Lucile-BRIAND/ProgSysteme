﻿#pragma checksum "..\..\ExecuteJobView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A9D2D8B77F9D0921CA0C82CE966423B6F4C4CF702085EEEF5CF6993E9A81B504"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using AppV2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AppV2 {
    
    
    /// <summary>
    /// ExecuteJobView
    /// </summary>
    public partial class ExecuteJobView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\ExecuteJobView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button executeBackupButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\ExecuteJobView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mainMenuButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ExecuteJobView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid executeJobDataGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ExecuteJobView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox JobSoftwareNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ExecuteJobView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock JobSoftwareNameTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AppV2;component/executejobview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ExecuteJobView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.executeBackupButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\ExecuteJobView.xaml"
            this.executeBackupButton.Click += new System.Windows.RoutedEventHandler(this.ButtonExecuteJob_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainMenuButton = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\ExecuteJobView.xaml"
            this.mainMenuButton.Click += new System.Windows.RoutedEventHandler(this.ButtonMainMenu_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.executeJobDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\ExecuteJobView.xaml"
            this.executeJobDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 16 "..\..\ExecuteJobView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExecuteAllBackupButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.JobSoftwareNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.JobSoftwareNameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
