using MartianRobots.Application.DTOs;
using System.Collections.Generic;

namespace MartianRobots.Application.DTOs
{
    public class UserInputDTO
    {
        public GridCoordinatesDTO GridCoordinates { get; set; }
        public List<RobotDTO> RobotOperations { get; set; }
    }
}
