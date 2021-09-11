using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Application.Models
{
    public class RobotOperations
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Orientation { get; set; }
        public string Instructions { get; set; }
        public string Status { get; set; }
    }
}
