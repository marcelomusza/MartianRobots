using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Infrastructure.Repository
{
    public class RobotMovementsRepository : Repository <RobotMovements>, IRobotMovementsRepository
    {

        private MartianRobotsDBContext ctx;

        public RobotMovementsRepository(MartianRobotsDBContext context)
            : base(context)
        {
            ctx = context;
        }

        public MartianRobotsDBContext MartianRobotsDBContext
        {
            get { return Context as MartianRobotsDBContext; }
        }
    }
}
