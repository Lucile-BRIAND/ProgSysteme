﻿#pragma checksum "..\..\GeneralSettingsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "95F5D16FCD9D8BA9BBB52F35A5E90008A1C22A8C47125297943AC49BB0181EE1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using AppV3;
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


namespace AppV3 {
    
    
    /// <summary>
    /// GeneralSettingsView
    /// </summary>
    public partial class GeneralSettingsView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox selectedLanguageComboBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox jobSoftawreNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label jobSoftwareLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox LogFileFormatComboBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckboxPNG;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckboxJPG;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckboxPDF;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckboxTXT;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\GeneralSettingsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox maximumFileSizeComboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/AppV2;component/generalsettingsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GeneralSettingsView.xaml"
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
            this.selectedLanguageComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\GeneralSettingsView.xaml"
            this.selectedLanguageComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.jobSoftawreNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.jobSoftwareLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.LogFileFormatComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\GeneralSettingsView.xaml"
            this.LogFileFormatComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LogFileFormatComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 22 "..\..\GeneralSettingsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSelectJobSoftware_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 23 "..\..\GeneralSettingsView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonMainMenu_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CheckboxPNG = ((System.Windows.Controls.CheckBox)(target));
            
            #line 25 "..\..\GeneralSettingsView.xaml"
            this.CheckboxPNG.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 25 "..\..\GeneralSettingsView.xaml"
            this.CheckboxPNG.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CheckboxJPG = ((System.Windows.Controls.CheckBox)(target));
            
            #line 26 "..\..\GeneralSettingsView.xaml"
            this.CheckboxJPG.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 26 "..\..\GeneralSettingsView.xaml"
            this.CheckboxJPG.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CheckboxPDF = ((System.Windows.Controls.CheckBox)(target));
            
            #line 27 "..\..\GeneralSettingsView.xaml"
            this.CheckboxPDF.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 27 "..\..\GeneralSettingsView.xaml"
            this.CheckboxPDF.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CheckboxTXT = ((System.Windows.Controls.CheckBox)(target));
            
            #line 28 "..\..\GeneralSettingsView.xaml"
            this.CheckboxTXT.Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 28 "..\..\GeneralSettingsView.xaml"
            this.CheckboxTXT.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.maximumFileSizeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\GeneralSettingsView.xaml"
            this.maximumFileSizeComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MaximumFileSizeComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

