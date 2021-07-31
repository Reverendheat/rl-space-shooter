using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Raylib_Adventures.Interfaces
{
    interface ISpawner
    {
        public void SetupTimer(double d);
        public void Spawn(Object source, ElapsedEventArgs e);
    }
}
