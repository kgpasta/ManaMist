using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public class TownCenter : Building
    {
        public bool CanBuild(Coordinate currentCoordinate, Coordinate coordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(currentCoordinate) && entity is Worker;
        }
    }
}