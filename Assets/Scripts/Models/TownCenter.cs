using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public class TownCenter : Building
    {
        public TownCenter() : base("TownCenter", new Cost(), 10)
        {
            BuildAction buildAction = new BuildAction(CanBuild);
            this.AddAction(buildAction);
        }

        public bool CanBuild(Coordinate currentCoordinate, Coordinate coordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(currentCoordinate) && entity.name == "Worker";
        }
    }
}