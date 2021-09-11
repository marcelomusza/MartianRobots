using MartianRobots.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.DTOs
{
    public class UserOutput
    {
        public UserOutput()
        {
            RobotResult = new List<RobotOperations>();
        }
        public List<RobotOperations> RobotResult { get; set; }

    }
}
