using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class BuildAction : Action
    {
        public CanBuildFunction CanBuild;

        public delegate bool CanBuildFunction(Coordinate currentCoordinate, Coordinate buildingCoordinate, Entity target);

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            Coordinate startCoordinate = mapController.GetPositionOfEntity(entity.id);
            return CanBuild(startCoordinate, coordinate, target);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            mapController.AddToMap(coordinate, target);
            player.AddEntity(target);
        }
    }
}