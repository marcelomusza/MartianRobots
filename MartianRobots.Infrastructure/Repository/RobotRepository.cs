using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using System;
using System.Linq;

namespace MartianRobots.Infrastructure.Repository
{
    public class RobotsRepository : Repository<Robot>, IRobotsRepository
    {

        private MartianRobotsDBContext ctx;

        public RobotsRepository(MartianRobotsDBContext context)
            : base(context)
        {
            ctx = context;
        }

        public MartianRobotsDBContext MartianRobotsDBContext
        {
            get { return Context as MartianRobotsDBContext; }
        }

        public Robot GetRobotByName(string robotName)
        {
            return ctx.Robots
                .Where(x => x.Name.Equals(robotName))
                .SingleOrDefault();
        }
    }
}
