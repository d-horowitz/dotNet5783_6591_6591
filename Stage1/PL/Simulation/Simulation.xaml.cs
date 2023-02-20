using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace PL.Simulation
{
    /// <summary>
    /// Interaction logic for Simulation.xaml
    /// </summary>
    public partial class Simulation : Window, INotifyPropertyChanged
    {
        private bool timing = true;
        private Thread timer;
        private DateTime start = DateTime.Now;
        private BackgroundWorker bw;
        public Simulation()
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            //TimeDisplay.DataContext = new { t = DateTime.Now - start };
            TimeDisplay.DataContext = new { t = DateTime.Now.ToLongTimeString() };
            bw = new();
            timer = new(runTimer);
            timer.Start();
        }
        private void runTimer()
        {
            while (timing)
            {
                UpdateTime(this, new RoutedEventArgs());
                Thread.Sleep(1000);
            }
        }

        //==================Hide X Button==================>>
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        public event PropertyChangedEventHandler? PropertyChanged;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        //==================Hide X Button==================^^

        private void StopSimulation(object sender, RoutedEventArgs e)
        {
            timing = false;
            Close();
        }
        private void UpdateTime(object sender, RoutedEventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(UpdateTime, sender, e);
            }
            else
            {
                //TimeDisplay.DataContext = new { t = (DateTime.Now - start).ToString().Substring(0, 8) };
                TimeDisplay.DataContext = new { t = DateTime.Now.ToLongTimeString() };
            }
        }

    }
}