﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D550181EE2FE68B871729DA285F0A4A6"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using _20120608HandTrackingLibrary;


namespace _20120611QuoteComplete {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal _20120608HandTrackingLibrary.KinectButton kinectButton1;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal _20120608HandTrackingLibrary.HoverButton hoverButton1;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal _20120608HandTrackingLibrary.MagnetButton magnetButton1;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\MainWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/20120611QuoteComplete;component/mainwindow.xaml", System.UriKind.Relative);
            
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
            this.kinectButton1 = ((_20120608HandTrackingLibrary.KinectButton)(target));
            
            #line 6 "..\..\..\MainWindow.xaml"
            this.kinectButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\MainWindow.xaml"
            this.kinectButton1.KinectCursorLeave += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.hoverButton1 = ((_20120608HandTrackingLibrary.HoverButton)(target));
            
            #line 12 "..\..\..\MainWindow.xaml"
            this.hoverButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\MainWindow.xaml"
            this.hoverButton1.KinectCursorLeave += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.magnetButton1 = ((_20120608HandTrackingLibrary.MagnetButton)(target));
            
            #line 13 "..\..\..\MainWindow.xaml"
            this.magnetButton1.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\MainWindow.xaml"
            this.magnetButton1.KinectCursorLeave += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBox1 = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            
            #line 16 "..\..\..\MainWindow.xaml"
            ((_20120608HandTrackingLibrary.MagneticSlide)(target)).KinectCursorEnter += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\MainWindow.xaml"
            ((_20120608HandTrackingLibrary.MagneticSlide)(target)).KinectCursorLeave += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 18 "..\..\..\MainWindow.xaml"
            ((_20120608HandTrackingLibrary.PushButton)(target)).KinectCursorMove += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\MainWindow.xaml"
            ((_20120608HandTrackingLibrary.PushButton)(target)).KinectCursorLeave += new _20120608HandTrackingLibrary.KinectCursorEventHandler(this.Button_KinectCursorLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

