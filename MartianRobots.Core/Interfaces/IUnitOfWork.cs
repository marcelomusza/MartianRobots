using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRobotMovementsRepository RobotMovements { get; }
        int Complete();
    }
}
