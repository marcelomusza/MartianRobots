using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Interfaces
{
    public interface IRobotMovementsRepository : IRepository<RobotMovements>
    {
        IEnumerable<RobotMovements> GetListOfRobots();
        RobotMovements GetRobotByName(string name);
    }
}
