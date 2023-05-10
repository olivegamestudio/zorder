using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 _window;

        readonly DispatcherTimer _timer = new();

        public MainWindow()
        {
            InitializeComponent();

            _window = new();
            _window.Show();

            _timer.Tick += TimerOnTick;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }

        void TimerOnTick(object? sender, EventArgs e)
        {
            Title = WindowChecker.IsWindowOnTop(new WindowInteropHelper(this).Handle,
                new WindowInteropHelper(_window).Handle) ? "OnTop" : "";
        }
    }

    class WindowChecker
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        const int GW_HWNDNEXT = 2;

        public static bool IsWindowOnTop(IntPtr window, IntPtr test)
        {
            do
            {
                window = GetWindow(window, GW_HWNDNEXT);
                if (window == test)
                {
                    return true;
                }
            }
            while (window != IntPtr.Zero);
            
            return false;
        }
    }
}
