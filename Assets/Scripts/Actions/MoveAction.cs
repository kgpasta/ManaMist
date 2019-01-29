using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/MoveAction")]
    public class MoveAction : Action, ISelectableTargetAction
    {
        public int movementRange;
        public List<Models.Terrain> allowedTerrain;

        public int Range => movementRange;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            return base.CanExecute(player, entity, targetCoordinate, target)
            && allowedTerrain.Contains(mapTile.terrain)
            && mapTile.entities.Count == 0;
        }

        public override void Execute(Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            base.Execute(player, entity, coordinate, target);

            mapController.MoveEntity(coordinate, entity);
        }
    }
}