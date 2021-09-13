using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Interfaces
{
    public interface IRobotsRepository : IRepository<Robot>
    {
        Robot GetRobotByName(string name);
    }
}
