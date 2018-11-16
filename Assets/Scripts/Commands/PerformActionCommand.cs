using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Commands
{
    public class PerformActionCommand : Command
    {
        private Coordinate coordinate;
        private Entity target;
        private ActionType actionType;

        public PerformActionCommand(int playerId, Coordinate coordinate, Entity target, ActionType actionType) : base(playerId, CommandType.PERFORMACTION)
        {
            this.coordinate = coordinate;
            this.target = target;
            this.actionType = actionType;
        }

        public bool Execute(MapController mapController, Player player, Entity entity)
        {
            Coordinate currentCoordinate = mapController.GetPositionOfEntity(entity.id);

            Action action = entity.GetAction(actionType);

            if (action != null)
            {
                if (action.CanExecute(mapController, player, entity, this.coordinate, this.target))
                {
                    action.Execute(mapController, player, entity, this.coordinate, this.target);
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return "Performing action " + actionType + " at " + coordinate.ToString() + " with target " + target;
        }
    }
}