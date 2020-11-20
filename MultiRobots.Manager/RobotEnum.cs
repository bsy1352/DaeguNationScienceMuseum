using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiRobots.Manager
{
    public enum Robots
    {
        R1,
        R2,
        R3
    }

    public enum Direction
    {
        Forward,
        Backward,
        None
    }

    public enum StepType
    {
        None,
        Full,
        Bonnet,
        Wheel,
        Door
    }
}
