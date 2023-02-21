using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simulator;
public static class Simulator
{
    private static readonly IBl bl = Factory.Get();
    private static volatile bool keepSimulating = true;
    public static event EventHandler SimulationStopCompleted;
    private static Thread? thread;
    private static int x = 0;


    public static void Run(object? sender, DoWorkEventArgs args)
    {
        thread = new(() =>
        {
            while (keepSimulating)
            {
                x++;
                Thread.Sleep(1000);
            }
        }
        );
        thread.Start();
    }
    public static void Stop(object? sender, RunWorkerCompletedEventArgs args)
    {
        keepSimulating = false;
        SimulationStopCompleted.Invoke(null, EventArgs.Empty);
    }
}

