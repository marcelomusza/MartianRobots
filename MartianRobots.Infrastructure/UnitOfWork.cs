using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using MartianRobots.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRobotMovementsRepository RobotMovements { get; private set; }
        private readonly MartianRobotsDBContext _context;

        public UnitOfWork(MartianRobotsDBContext context)
        {
            _context = context;
            RobotMovements = new RobotMovementsRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
