using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{
    public class RobotMovementsOutputDTO
    {
        public string Name { get; set; }
        public char Orientation { get; set; }
        public char Instruction { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool Dead { get; set; }
    }
}
