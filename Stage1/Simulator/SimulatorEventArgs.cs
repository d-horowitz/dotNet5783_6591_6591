using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;
public class SimulatorEventArgs : EventArgs
{
    public int RandomTime;
    public SimulatorEventArgs(int randomTime)
    {
        RandomTime = randomTime;
    }
}

