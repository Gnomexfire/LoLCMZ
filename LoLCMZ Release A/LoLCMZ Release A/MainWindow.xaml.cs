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
using System.Threading;
using System.IO;
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

        public INTERNA_BASE_SOURCE Base = new INTERNA_BASE_SOURCE();

        public COMPLEX _COMPLEX = new COMPLEX();

        public SOURCE_UTILIDADE _SOURCE_UTILIDADE = new SOURCE_UTILIDADE();

        static IsNOP_Status _StatusGame = IsNOP_Status.Indefinido;
        enum IsNOP_Status
        {
            Esperando,
            Outdata,
            Funciona,
            Completo,
            Indefinido,
            Error_INES
        }

        Mutex _AppMutex;

        public static string _About_Message;
        #endregion

        #region Program
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
            #region Mutex
            // Initialize Mutex OnePrev Instace Application
            bool _MutexNew = false;
            _AppMutex = new Mutex(false, "LoLCMZ", out _MutexNew);
            if(!_MutexNew)
            {
                App.Current.Shutdown();
            }
            #endregion
            
            _StatusGame = IsNOP_Status.Indefinido;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            _KeyBoardHook.Install();
            _KeyBoardHook.KeyDown += _KeyBoardHook_KeyDown;
            _KeyBoardHook.KeyUp += _KeyBoardHook_KeyUp;

            LBL_GAME_VERSIO_CORE.Content = Base._LEGUE_OF_LEGENDS_VERSION_WINDOW_SOURCE;

            // I GO Beta
            Base._BASE_FROM_COMPILED_CHECK_VERSION();
            LBL_DLL_COMPILED.Content = Base._BASE_FROM_COMPILED;

           
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Process[] _Proc = Process.GetProcessesByName("League Of Legends");
            if (_Proc.Length == 0)
            {
                LBL_STATUS_WORK.Content = "Wait . . .";
                _StatusGame = IsNOP_Status.Esperando;
                return;
            }
            // GetPath
            Base._LEAGUE_OF_LEGENDS_PATH_EXE = _Proc[0].Modules[0].FileName;
            Base._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL = Util.GetMD5HashOfAFile(Base._LEAGUE_OF_LEGENDS_PATH_EXE);
            SOLUTION_BASE_SOURCE._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL = Base._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL;
            // Creat XML Update.XML Open 1 Instace - Depois não Faz  ...
            //if(Util.Update_Existe()==false)
            //{
                //Console.WriteLine("Criar Update.XML");
                // Call Event criar Arquivo XML 1 Vez
                //Util.Criar_Arq_XML();
            //}
            //Console.WriteLine("Já Existe Update.XML");
            // Process create check Md5
            if(Base._LEAGUE_OF_LEGENDS_VERSION_CORE_MD5_SOURCE != Base._LEAGUE_OF_LEGENDS_VERSION_MD5_INSTALL)
            {
                LBL_STATUS_WORK.Content = "Outdated";
                LBL_STATUS_WORK.Foreground = Brushes.Red;
                dispatcherTimer.Stop();
                _StatusGame = IsNOP_Status.Outdata;
                return;
            }

            if (_StatusGame != IsNOP_Status.Completo)
            {
                LBL_STATUS_WORK.Content = "Working";
                _StatusGame = IsNOP_Status.Funciona;
            }

            if(CHK_TESTE.IsChecked.Value)
            {
                if (Base.IsNOP == true)
                {
                    //LBL_STATUS_WORK.Content = "Completed !";
                    //_StatusGame = IsNOP_Status.Completo;
                    return;
                }
                // Injection Call Module DLL
                _COMPLEX.Inject_NOP();
                LBL_STATUS_WORK.Content = "Completed !";
                _StatusGame = IsNOP_Status.Completo;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Teste Load XML
            // _SOURCE_UTILIDADE.Load_Teste_Version();
            // Util.Criar_Arq_XML();

            //MessageBox.Show("Go Update . . . wait seconds ");
            //App.Current.Shutdown();
            //
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\IGOUpdate.exe")) { System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\IGOUpdate.exe",Util._GET_URL_FILE_DOWNLOAD_LINK()); }
            //
        }
        #endregion

        private void CMD_UPDATE_Click(object sender, RoutedEventArgs e)
        {
            if (Util.Load_Teste_Version())
            {
                MessageBox.Show("Go Update . . . wait seconds ");
                App.Current.Shutdown();
                //
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\IGOUpdate.exe")) { System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\IGOUpdate.exe", Util._GET_URL_FILE_DOWNLOAD_LINK()); }
                //
            }
            else
            {
                MessageBox.Show("No update");
                //Console.WriteLine("Sem Atualizacao");
            }
        }

        private void CMD_ABOUT_Click(object sender, RoutedEventArgs e)
        {
            _About_Message = Util._About();
            MessageAbout _Window = new MessageAbout();
            _Window.ShowDialog();
        }

        private void CMD_SUPPORT_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Util.Get_Support_Url());
        }

       

    }
}
