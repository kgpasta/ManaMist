using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public class Worker : Unit
    {
        public bool CanBuild(Coordinate currentCoordinate, Coordinate buildingCoordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(buildingCoordinate) && entity is Building;
        }
    }
}