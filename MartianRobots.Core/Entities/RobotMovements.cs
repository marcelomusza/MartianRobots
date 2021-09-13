using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Entities
{
    public class RobotMovements
    {

        public int Id { get; set; }
        public char Orientation { get; set; }
        public char Instruction { get; set; }
        public bool Dead { get; set; }

        //Relationships
        public Robot Robot { get; set; }
        public int RobotId { get; set; }

        public Position Position { get; set; }
        public int PositionId { get; set; }

        public GridCoordinate GridCoordinates { get; set; }
        public int GridCoordinatesId { get; set; }
    }
}
