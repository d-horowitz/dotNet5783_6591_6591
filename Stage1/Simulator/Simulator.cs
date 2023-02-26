using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simulator;

public delegate void ProgressUpdatedEventHandler(SimulatorEventArgs args);
public delegate void SimulationStopCompletedEventHandler();
public static class Simulator
{
    private static readonly IBl bl = Factory.Get();
    private static volatile bool keepSimulating = true;
    private static event SimulationStopCompletedEventHandler? SimulationStopCompleted;
    private static event ProgressUpdatedEventHandler? ProgressUpdated;
    private static Thread? thread;
    private static readonly Random randomer = new();

    public static void Run(object? sender, DoWorkEventArgs args)
    {
        thread = new(() =>
        {
            while (keepSimulating)
            {
                nextOrder();
            }
        }
        );
        thread.Start();
    }

    private static void nextOrder()
    {
        int? id = bl.Order.NextOrder();
        if (id == null)
        {
            Stop();
        }
        else
        {
            BO.Order order = bl.Order.Read((int)id);
            if (order.Status == BO.EOrderStatus.Processed)
            {
                bl.Order.UpdateShipping(order.Id);
            }
            else
            {
                bl.Order.UpdateDelivery(order.Id);
            }
            int random = randomer.Next(5, 15);
            ProgressUpdated?.Invoke(new SimulatorEventArgs(random, order.Id, order.Status));
            Thread.Sleep(random * 1000);
        }
    }
    public static void Stop()
    {
        keepSimulating = false;
        SimulationStopCompleted?.Invoke();
    }

    public static void SimulationStopCompletedRegister(SimulationStopCompletedEventHandler eh) => SimulationStopCompleted += eh;

    public static void SimulationStopCompletedUnregister(SimulationStopCompletedEventHandler eh) => SimulationStopCompleted -= eh;

    public static void ProgressUpdatedRegister(ProgressUpdatedEventHandler eh) => ProgressUpdated += eh;

    public static void ProgressUpdatedUnregister(ProgressUpdatedEventHandler eh) => ProgressUpdated -= eh;
}

