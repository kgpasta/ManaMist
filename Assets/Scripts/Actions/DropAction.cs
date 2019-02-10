using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/Drop Action")]
    public class DropAction : Action, ISelectableTargetAction
    {
        public Entity entityToDrop;
        public int Range => 1;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target = null)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            MoveAction moveAction = entityToDrop.GetAction<MoveAction>();

            return base.CanExecute(player, entity, targetCoordinate, target)
            && mapTile.entities.Count == 0
            && moveAction != null
            && moveAction.allowedTerrain.Contains(mapTile.terrain);
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target = null)
        {
            base.Execute(player, entity, targetCoordinate, target);

            mapController.AddToMap(targetCoordinate, entityToDrop);
            entityToDrop = null;
        }
    }
}