using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{ 
    public class RobotMovementsDTO
    {

        public int Id { get; set; }
        public char Orientation { get; set; }
        public char Instruction { get; set; }
        public bool Dead { get; set; }

        //Relationships
        public RobotDTO Robot { get; set; }
        public int RobotId { get; set; }

        public PositionDTO Position { get; set; }
        public int PositionId { get; set; }

        public GridCoordinatesDTO GridCoordinates { get; set; }
        public int GridCoordinatesId { get; set; }
    }
}
