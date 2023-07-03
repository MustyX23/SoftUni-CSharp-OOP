using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots;

        public RobotRepository()
        {
            robots = new List<IRobot>();
        }

        public IReadOnlyCollection<IRobot> Models()
        {
            return robots.AsReadOnly();
        }

        public void AddNew(IRobot robot)
        {
            robots.Add(robot);
        }

        public bool RemoveByName(string robotModel)
        {
            IRobot robot = robots.FirstOrDefault(r => r.Model == robotModel);

            if (robot != null)
            {
                robots.Remove(robot);
                return true;
            }

            return false;
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return robots.FirstOrDefault(r => r.InterfaceStandards.Contains(interfaceStandard));
        }

    }
}
