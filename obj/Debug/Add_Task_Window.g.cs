﻿#pragma checksum "..\..\Add_Task_Window.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "964C0627B0B5DEA7D60D61FF682C1569698CFB58A7A9AAE202AE0A8A229530E5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using Kalendarz;
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


namespace Kalendarz {
    
    
    /// <summary>
    /// Add_Task_Window
    /// </summary>
    public partial class Add_Task_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Bar;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox User_Text;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox User_Date;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Accept;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\Add_Task_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Decline;
        
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
            System.Uri resourceLocater = new System.Uri("/Kalendarz;component/add_task_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Add_Task_Window.xaml"
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
            this.Bar = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\Add_Task_Window.xaml"
            this.Bar.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Bar_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\Add_Task_Window.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.User_Text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.User_Date = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Accept = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\Add_Task_Window.xaml"
            this.Accept.Click += new System.Windows.RoutedEventHandler(this.Accept_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Decline = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\Add_Task_Window.xaml"
            this.Decline.Click += new System.Windows.RoutedEventHandler(this.Decline_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

