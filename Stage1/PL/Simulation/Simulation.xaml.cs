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
    public partial class Simulation : Window, INotifyPropertyChanged
    {
        private DateTime start = DateTime.Now;
        private BackgroundWorker bw;
        private readonly IBl bl;
        public Simulation(IBl p_bl)
        {
            InitializeComponent();
            Loaded += ToolWindow_Loaded;
            TimeDisplay.DataContext = new { t = DateTime.Now.ToLongTimeString() };
            bl = p_bl;
            MainGrid.DataContext = new { OrderId = 0, Previous = BO.EOrderStatus.Processed, Next = BO.EOrderStatus.Processed };
            Simulator.Simulator.ProgressUpdatedRegister(ProgressUpdated);
            bw = new();
            bw.DoWork += Simulator.Simulator.Run;
            bw.DoWork += runTimer;
            bw.RunWorkerCompleted += Simulator.Simulator.Stop;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();

        }
        private void runTimer(object? sender, DoWorkEventArgs args)
        {
            while (!bw.CancellationPending)
            {
                UpdateTime(sender, args);
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

        private void ProgressUpdated(SimulatorEventArgs args)
        {
            Dispatcher.BeginInvoke(() => MainGrid.DataContext = new { OrderId = 0, Previous = BO.EOrderStatus.Processed, Next = BO.EOrderStatus.Processed, UpdateTime = args.RandomTime });

        }

        private void StopSimulation(object sender, RoutedEventArgs e)
        {
            bw.CancelAsync();
            Close();
        }
        private void NextOrder(object sender, RoutedEventArgs e)
        {
            int? id = bl.Order.NextOrder();
            MessageBox.Show(id.ToString() ?? "No Order");
            BO.Order order = bl.Order.Read(id ?? 0);
            MainGrid.DataContext = new { OrderId = order.Id, Previous = order.Status, Next = (BO.EOrderStatus)(Convert.ToInt32(order.Status) + 1), UpdateTime = 0 };
        }
        private void UpdateTime(object? sender, DoWorkEventArgs e)
        {
            Dispatcher.BeginInvoke(() => TimeDisplay.DataContext = new { t = (DateTime.Now - start).ToString().Substring(0, 8) });
            /*if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(UpdateTime, sender, e);
            }
            else
            {
                //TimeDisplay.DataContext = new { t = (DateTime.Now - start).ToString().Substring(0, 8) };
                TimeDisplay.DataContext = new { t = DateTime.Now.ToLongTimeString() };
            }*/
        }

    }
}