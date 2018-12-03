using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Commands
{
    public class PerformActionCommand<T> : Command where T : Action
    {
        private Coordinate coordinate;
        private Entity target;

        public PerformActionCommand(int playerId, Coordinate coordinate, Entity target) : base(playerId, CommandType.PERFORMACTION)
        {
            this.coordinate = coordinate;
            this.target = target;
        }

        public bool Execute(MapController mapController, Player player, Entity entity)
        {
            Coordinate currentCoordinate = mapController.GetPositionOfEntity(entity.id);

            Action action = entity.GetAction<T>();

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
            return "Performing action " + nameof(T) + " at " + coordinate.ToString() + " with target " + target;
        }
    }
}