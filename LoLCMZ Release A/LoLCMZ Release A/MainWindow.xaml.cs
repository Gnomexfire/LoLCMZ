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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps;
using MahApps.Metro.Controls;
using KDMemory;
using System.Diagnostics;

using LoLCMZ_Base;
namespace LoLCMZ_Release_A
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region DECLARE
        KeyboardHook _KeyBoardHook = new KeyboardHook();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public SOLUTION_BASE_SOURCE Base = new SOLUTION_BASE_SOURCE();
        public COMPLEX _COMPLEX = new COMPLEX();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CHK_TESTE_Checked(object sender, RoutedEventArgs e)
        {
            CHK_TESTE.Content = "Desativar";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            _KeyBoardHook.Install();
            _KeyBoardHook.KeyDown += _KeyBoardHook_KeyDown;
            _KeyBoardHook.KeyUp += _KeyBoardHook_KeyUp;

            LBL_GAME_VERSIO_CORE.Content = Base._LEGUE_OF_LEGENDS_VERSION_WINDOW_SOURCE;

            // I GO Test
            Base._BASE_FROM_COMPILED_CHECK_VERSION();
            LBL_DLL_COMPILED.Content = Base._BASE_FROM_COMPILED;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            Process[] _Proc = Process.GetProcessesByName("League Of Legends");
            if (_Proc.Length == 0)
            {
                LBL_STATUS_WORK.Content = "Wait . . .";
                return;
            }
            // GetPath
            Base._LEAGUE_OF_LEGENDS_PATH_EXE = _Proc[0].Modules[0].FileName;
            Base._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL = Util.GetMD5HashOfAFile(Base._LEAGUE_OF_LEGENDS_PATH_EXE);
            // Process create check Md5
            if(Base._LEAGUE_OF_LEGENDS_VERSION_CORE_MD5_SOURCE != Base._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL)
            {
                LBL_STATUS_WORK.Content = "Outdated";
                LBL_STATUS_WORK.Foreground = Brushes.Red;
                dispatcherTimer.Stop();
                return;
            }
            LBL_STATUS_WORK.Content = "Working";
            
            if(CHK_TESTE.IsChecked.Value)
            {
                if (Base.IsNOP == true)
                {
                    return;
                }
                // Injection Call Module DLL
                _COMPLEX.Inject_NOP();
                //
            }
        }
        
        void _KeyBoardHook_KeyUp(KeyboardHook.VKeys key)
        {
            if (CHK_TESTE.IsChecked.Value)
            {
                if (key == KeyboardHook.VKeys.ADD)
                {
                    _COMPLEX.MaxAndMinZoom(true);
                }
            }
        }

        void _KeyBoardHook_KeyDown(KeyboardHook.VKeys key)
        {
            if (CHK_TESTE.IsChecked.Value)
            {
                if (key == KeyboardHook.VKeys.SUBTRACT)
                {
                    _COMPLEX.MaxAndMinZoom(false);
                }
            }
        }

        private void CHK_TESTE_Unchecked(object sender, RoutedEventArgs e)
        {
            CHK_TESTE.Content = "Ativar";
        }
    }
}
