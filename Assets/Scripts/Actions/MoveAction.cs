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

        public delegate bool CanMoveFunction(Coordinate coordinate);

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            return CanMove(coordinate);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            mapController.MoveEntity(coordinate, entity);
        }
    }
}