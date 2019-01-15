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

        public override bool Execute(MapController mapController, TurnController turnController, Player player)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
            Entity entity = mapTile.entities[0];

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