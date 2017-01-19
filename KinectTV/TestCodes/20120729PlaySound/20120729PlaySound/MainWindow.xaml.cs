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
using System.Media;

namespace _20120729PlaySound
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void channel1_Click(object sender, RoutedEventArgs e)
        {
            Play("Channel1.WAV");
            

        }

        private void Play(String name)
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                string location = System.Environment.CurrentDirectory + "\\Voices\\" + name;
                player.SoundLocation = location;
                player.Play();
            }
        }

        private void channel2_Click(object sender, RoutedEventArgs e)
        {
            Play("Channel2.WAV");
        }
    }
}
