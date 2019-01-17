using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/MoveAction")]
    public class MoveAction : Action
    {
        public int movementRange;
        public List<Models.Terrain> allowedTerrain;

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
            return base.CanExecute(mapController, player, entity, coordinate, target) && CanMove(mapTile);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            base.Execute(mapController, player, entity, coordinate, target);

            mapController.MoveEntity(coordinate, entity);
        }

        public bool CanMove(MapTile mapTile)
        {
            return allowedTerrain.Contains(mapTile.terrain) && mapTile.entities.Count == 0;
        }
    }
}