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
    public class BuildAction : Action, ISelectableTargetAction
    {
        public List<EntityType> canBuildList;

        public int Range => 1;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            List<IBuildConstraint> buildConstraints = target.GetActions<IBuildConstraint>();

            return base.CanExecute(player, entity, targetCoordinate, target)
            && player.resources.CanDecrement(target.Cost)
            && canBuildList.Contains(target.Type)
            && buildConstraints.All(constraint => constraint.CanBuild(player, mapTile));
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            base.Execute(player, entity, targetCoordinate, target);

            player.resources.Decrement(target.Cost);
            player.AddEntity(target);
            mapController.AddToMap(targetCoordinate, target);
        }
    }

    public interface IBuildConstraint
    {
        bool CanBuild(Player player, MapTile mapTile);
    }
}