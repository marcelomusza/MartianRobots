using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Application.Models
{
    public class UserInput
    {
        public GridCoordinates GridCoordinates { get; set; }

        public List<RobotOperations> RobotOperations { get; set; }
    }
}
