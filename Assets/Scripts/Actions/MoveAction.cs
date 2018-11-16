using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class MoveAction : Action
    {
        public CanMoveFunction CanMove;

        public MoveAction(CanMoveFunction canMoveFunction) : base(ActionType.MOVE)
        {
            this.CanMove = canMoveFunction;
        }

        public delegate bool CanMoveFunction(Coordinate start, Coordinate end);

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            Coordinate startCoordinate = mapController.GetPositionOfEntity(entity.id);
            return CanMove(startCoordinate, coordinate);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            mapController.MoveEntity(coordinate, entity);
        }
    }
}