using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{
    public class GridCellDTO
    {
        public GridCellDTO()
        {
            Robot = new RobotDTO();
        }

        public RobotDTO Robot { get; set; }
    }
}
