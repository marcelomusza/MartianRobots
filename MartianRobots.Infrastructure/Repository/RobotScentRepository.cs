using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using MartianRobots.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Infrastructure.Repository
{
    public class RobotScentRepository : Repository<RobotScent>, IRobotScentRepository
    {

        private MartianRobotsDBContext ctx;

        public RobotScentRepository(MartianRobotsDBContext context)
            : base(context)
        {
            ctx = context;
        }

        public MartianRobotsDBContext MartianRobotsDBContext
        {
            get { return Context as MartianRobotsDBContext; }
        }


        public IEnumerable<RobotScent> GetAllRobotScentPositions()
        {
            return ctx.RobotScents
                    .ToList();
        }


    }
}
