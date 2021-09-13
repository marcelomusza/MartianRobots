using System.Collections.Generic;

namespace MartianRobots.Application.DTOs
{
    public class UserOutputDTO
    {
        public UserOutputDTO()
        {
            RobotResult = new List<RobotDTO>();
        }
        public List<RobotDTO> RobotResult { get; set; }

    }
}
