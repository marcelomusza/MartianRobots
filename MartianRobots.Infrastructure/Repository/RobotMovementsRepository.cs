using MartianRobots.Core.Entities;
using MartianRobots.Core.Interfaces;
using MartianRobots.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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



        public List<RobotMovements> GetListOfRobots()
        {
            return ctx.RobotMovements
                .Include(r => r.Robot)
                .Include(p => p.Position)
                .ToList();
        }

        public RobotMovements GetRobotByName(string robotName)
        {
            return ctx.RobotMovements
                .Include(r => r.Robot)
                .Include(p => p.Position)
                .Where(x => x.Robot.Name.Equals(robotName))
                .FirstOrDefault();
        }
    }
}
