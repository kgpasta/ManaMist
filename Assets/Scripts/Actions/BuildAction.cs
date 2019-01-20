using System;
using System.Collections.Generic;
using System.Linq;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/BuildAction")]
    public class BuildAction : Action
    {
        public List<EntityType> canBuildList;
        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            Entity target = mapTile.entities[0];
            List<IBuildConstraint> buildConstraints = target.GetActions<IBuildConstraint>();

            return base.CanExecute(player, entity, targetCoordinate)
            && canBuildList.Contains(target.type)
            && buildConstraints.All(constraint => constraint.CanBuild(mapTile));
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate)
        {
            base.Execute(player, entity, targetCoordinate);
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            Entity target = mapTile.entities[0];

            mapController.AddToMap(targetCoordinate, target);
            player.AddEntity(target);
        }
    }

    public interface IBuildConstraint
    {
        bool CanBuild(MapTile mapTile);
    }
}