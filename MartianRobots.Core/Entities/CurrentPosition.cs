using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Entities
{
    public class CurrentPosition
    {
        public int Id { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }

        //Relationship
        public int RobotId { get; set; }
        public Robot Robot { get; set; }
    }
}
