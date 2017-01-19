using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace _20120614ConnectArduino
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort = new SerialPort();

        public MainWindow()
        {
            InitializeComponent();

            SerialPort _tempPort;
            String[] PortName = SerialPort.GetPortNames();

            foreach (string str in PortName)
            {
                try
                {
                    _tempPort = new SerialPort(str);
                    _tempPort.Open();
                    if (_tempPort.IsOpen)
                    {
                        PortNames.Items.Add(str);
                        _tempPort.Close();
                    }
                }
                catch (Exception e)
                { }
            }
        }

        private void Ligten_Click(object sender, RoutedEventArgs e)
        {
            if (PortNames.Text == "")
            {
                return;
            }
            else
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.PortName = PortNames.Text;
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write("O");
                }
            }
        }

        private void Extinguish_Click(object sender, RoutedEventArgs e)
        {
            if (PortNames.Text == "")
            {
                return;
            }
            else
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.PortName = PortNames.Text;
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write("C");
                }
            }
        }


       
    }
}
