﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EDB2EA5D9E483B564055247C9E7D1863"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KinectGestureLibrary;
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


namespace _20120611FormatQuoteLibrary {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KinectGestureLibrary.KinectButton kinectButton1;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KinectGestureLibrary.HoverButton hoverButton1;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KinectGestureLibrary.MagnetButton magnetButton1;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/20120611FormatQuoteLibrary;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.kinectButton1 = ((KinectGestureLibrary.KinectButton)(target));
            
            #line 7 "..\..\..\MainWindow.xaml"
            this.kinectButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\MainWindow.xaml"
            this.kinectButton1.KinectCursorLeave += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.hoverButton1 = ((KinectGestureLibrary.HoverButton)(target));
            
            #line 8 "..\..\..\MainWindow.xaml"
            this.hoverButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\MainWindow.xaml"
            this.hoverButton1.KinectCursorLeave += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.magnetButton1 = ((KinectGestureLibrary.MagnetButton)(target));
            
            #line 9 "..\..\..\MainWindow.xaml"
            this.magnetButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\MainWindow.xaml"
            this.magnetButton1.KinectCursorLeave += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBox1 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            
            #line 12 "..\..\..\MainWindow.xaml"
            ((KinectGestureLibrary.MagneticSlide)(target)).KinectCursorEnter += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\MainWindow.xaml"
            ((KinectGestureLibrary.MagneticSlide)(target)).KinectCursorLeave += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 14 "..\..\..\MainWindow.xaml"
            ((KinectGestureLibrary.PushButton)(target)).KinectCursorMove += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\MainWindow.xaml"
            ((KinectGestureLibrary.PushButton)(target)).KinectCursorLeave += new KinectGestureLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

