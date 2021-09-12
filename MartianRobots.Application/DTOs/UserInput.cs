using MartianRobots.Application.DTOs;
using System.Collections.Generic;

namespace MartianRobots.Application.Models
{
    public class UserInput
    {
        public GridCoordinates GridCoordinates { get; set; }
        public List<Robot> RobotOperations { get; set; }
    }
}
