﻿#pragma checksum "..\..\AddWorkplaceWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CDC3D8A6EBF9554D110C145B51E08126306704FB520E8C6390FBDCB5BD5D5881"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using bd;


namespace bd {
    
    
    /// <summary>
    /// AddWorkplaceWindow
    /// </summary>
    public partial class AddWorkplaceWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView WorkplaceList;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ContextMenuBtn1;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ContextMenuDelBtn;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox LocationCmb;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WorkplaceTxb;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\AddWorkplaceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/bd;component/addworkplacewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddWorkplaceWindow.xaml"
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
            this.WorkplaceList = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.ContextMenuBtn1 = ((System.Windows.Controls.MenuItem)(target));
            
            #line 27 "..\..\AddWorkplaceWindow.xaml"
            this.ContextMenuBtn1.Click += new System.Windows.RoutedEventHandler(this.ContextMenuBtn1_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ContextMenuDelBtn = ((System.Windows.Controls.MenuItem)(target));
            
            #line 28 "..\..\AddWorkplaceWindow.xaml"
            this.ContextMenuDelBtn.Click += new System.Windows.RoutedEventHandler(this.ContextMenuDelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LocationCmb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.WorkplaceTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.AddBtn = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\AddWorkplaceWindow.xaml"
            this.AddBtn.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

