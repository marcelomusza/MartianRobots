using MartianRobots.Application.DTOs;
using MartianRobots.Application.Models;

namespace MartianRobots.Application.Interfaces
{
    public interface IMartianEngine
    {
        UserOutput OperateRobotsOnGrid(UserInput input);
    }
}
