using BlApi;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using Simulator;

namespace PL.Simulation
{
    /// <summary>
    /// Interaction logic for Simulation.xaml
    /// </summary>
    public partial class Simulation : Window
    {
        private readonly DateTime start = DateTime.Now;
        private readonly BackgroundWorker bw;
        public Simulation()
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            TimeDisplay.DataContext = new { t = DateTime.Now.ToLongTimeString() };
            MainGrid.DataContext = new
            {
                OrderId = 0,
                Previous = BO.EOrderStatus.Processed,
                Next = BO.EOrderStatus.Processed,
                UpdateTime = 0
            };
            bw = new();
            Simulator.Simulator.ProgressUpdatedRegister(ProgressUpdated);
            Simulator.Simulator.SimulationStopCompletedRegister(bw.CancelAsync);
            bw.DoWork += Simulator.Simulator.Run;
            bw.DoWork += runTimer;
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += UpdateTime;
            bw.RunWorkerCompleted += (sender, e) =>
            {
                Simulator.Simulator.ProgressUpdatedUnregister(ProgressUpdated);
                Simulator.Simulator.SimulationStopCompletedUnregister(bw.CancelAsync);
            };
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }
        private void runTimer(object? sender, DoWorkEventArgs args)
        {
            while (!bw.CancellationPending)
            {
                bw.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        //==================Hide X Button==================>>
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;


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

        private void ProgressUpdated(SimulatorEventArgs args)
        {
            Dispatcher.BeginInvoke(() => MainGrid.DataContext = new
            {
                args.OrderId,
                Previous = args.Status,
                Next = (BO.EOrderStatus)(Convert.ToInt32(args.Status) + 1),
                UpdateTime = args.RandomTime
            });
        }

        private void StopSimulation(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Stop();
            Close();
        }
        private void UpdateTime(object? sender, ProgressChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(
                () => TimeDisplay.DataContext = new { t = (DateTime.Now - start).ToString().Substring(0, 8) }
                );
        }
    }
}