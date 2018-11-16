using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public class Worker : Unit
    {
        public Worker() : base("Worker", new Cost(), 3)
        {
            BuildAction buildAction = new BuildAction(CanBuild);
            this.AddAction(buildAction);
        }

        public bool CanBuild(Coordinate currentCoordinate, Coordinate buildingCoordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(buildingCoordinate) && entity is Building;
        }
    }
}