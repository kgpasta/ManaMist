using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class MoveAction : Action
    {
        public int movementRange;
        public CanMoveFunction CanMove;

        public delegate bool CanMoveFunction(MapTile mapTile);

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
            return CanMove(mapTile);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            base.Execute(mapController, player, entity, coordinate, target);

            mapController.MoveEntity(coordinate, entity);
        }
    }
}