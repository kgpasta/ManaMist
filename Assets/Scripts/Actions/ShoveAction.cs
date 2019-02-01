using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/ShoveAction")]
    public class ShoveAction : Action, ISelectableTargetAction
    {
        public int Range => 1;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target = null)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);

            return base.CanExecute(player, entity, targetCoordinate, target)
            && mapTile.entities.Count > 0
            && player.GetEntity(mapTile.entities[0].id) != null
            && mapTile.entities[0].type.EntityClass != EntityClass.Building;
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target = null)
        {
            base.Execute(player, entity, targetCoordinate, target);
            Coordinate currentCoordinate = mapController.GetPositionOfEntity(entity.id);
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);

            Coordinate newCoordinate = GetDirectionOfShove(currentCoordinate, targetCoordinate);
            mapController.MoveEntity(newCoordinate, mapTile.entities[0]);
        }

        private Coordinate GetDirectionOfShove(Coordinate currentCoordinate, Coordinate targetCoordinate)
        {
            if (currentCoordinate.x > targetCoordinate.x)
            {
                return new Coordinate(targetCoordinate.x - 1, targetCoordinate.y);
            }
            else if (currentCoordinate.x < targetCoordinate.x)
            {
                return new Coordinate(targetCoordinate.x + 1, targetCoordinate.y);
            }
            else if (currentCoordinate.y > targetCoordinate.y)
            {
                return new Coordinate(targetCoordinate.x, targetCoordinate.y - 1);
            }
            else if (currentCoordinate.y < targetCoordinate.y)
            {
                return new Coordinate(targetCoordinate.x, targetCoordinate.y + 1);
            }

            return targetCoordinate;
        }
    }
}