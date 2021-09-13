using MartianRobots.Application.DTOs;

namespace MartianRobots.Application.Interfaces
{
    public interface IMartianEngine
    {
        UserOutputDTO OperateRobotsOnGrid(UserInputDTO input);
    }
}
