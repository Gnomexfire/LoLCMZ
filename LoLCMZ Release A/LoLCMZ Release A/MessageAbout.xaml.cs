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
using MahApps;
using MahApps.Metro.Controls;
namespace LoLCMZ_Release_A
{
    /// <summary>
    /// Interaction logic for MessageAbout.xaml
    /// </summary>
    public partial class MessageAbout : MetroWindow
    {
        public MessageAbout()
        {
            InitializeComponent();
        }

        private void LBL_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LBL.Content = LoLCMZ_Release_A.MainWindow._About_Message;
        }
    }
}
