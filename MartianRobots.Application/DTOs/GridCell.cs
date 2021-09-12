using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{
    public class GridCell
    {
        public GridCell()
        {
            Robot = new Robot();
        }

        public Robot Robot { get; set; }
    }
}
