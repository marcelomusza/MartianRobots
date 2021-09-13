using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Enums
{
    public static class Enumerations
    {
        public enum Instructions
        {
            //Basic Instructions, leaving room to add more
            R = 'R',
            L = 'L',
            F = 'F'
        }

        public enum Orientations
        {
            //Basic Orientations, leaving room to add more
            N = 'N',
            E = 'E',
            S = 'S',
            W = 'W'
        }
    }
}
