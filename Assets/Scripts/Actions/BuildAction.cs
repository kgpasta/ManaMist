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
        public override bool CanExecute(Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
            List<IBuildConstraint> buildConstraints = target.GetActions<IBuildConstraint>();

            return base.CanExecute(player, entity, coordinate, target)
            && canBuildList.Contains(target.type)
            && buildConstraints.All(constraint => constraint.CanBuild(mapTile));
        }

        public override void Execute(Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            base.Execute(player, entity, coordinate, target);

            mapController.AddToMap(coordinate, target);
            player.AddEntity(target);
        }
    }

    public interface IBuildConstraint
    {
        bool CanBuild(MapTile mapTile);
    }
}