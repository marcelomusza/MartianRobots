using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Interfaces
{
    public interface IRobotScentRepository : IRepository<RobotScent>
    {
        IEnumerable<RobotScent> GetAllRobotScentPositions();
    }
}
