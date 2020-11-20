using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiRobots.Net
{
    public interface ILogicQueue
    {
        void enqueue(string msg);
        Queue<string> getAll();
    }
}
