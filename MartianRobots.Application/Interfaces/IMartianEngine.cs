using MartianRobots.Application.DTOs;
using MartianRobots.Core.Entities;
using System.Collections.Generic;

namespace MartianRobots.Application.Interfaces
{
    public interface IMartianEngine
    {
        UserOutputDTO OperateRobotsOnGrid(UserInputDTO input);
        List<RobotMovementsOutputDTO> GetRobotMovements();
    }
}
