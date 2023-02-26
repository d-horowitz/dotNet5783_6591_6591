using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;
public class SimulatorEventArgs : EventArgs
{
    public int RandomTime;
    public int OrderId;
    public BO.EOrderStatus Status;
    public SimulatorEventArgs(int randomTime, int orderId, BO.EOrderStatus status)
    {
        RandomTime = randomTime;
        OrderId = orderId;
        Status = status;
    }
}

