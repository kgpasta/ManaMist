using System.Collections.Generic;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/Rescue Action")]
    public class RescueAction : Action, ISelectableTargetAction
    {
        [SerializeField] private List<EntityClass> rescuableClasses;
        public int Range => 1;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            DropAction dropAction = entity.GetAction<DropAction>();

            return base.CanExecute(player, entity, targetCoordinate, target)
            && dropAction != null
            && dropAction.entityToDrop == null
            && mapTile.entities.Count > 0
            && player.GetEntity(mapTile.entities[0].id) != null
            && rescuableClasses.Contains(mapTile.entities[0].type.EntityClass);
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            base.Execute(player, entity, targetCoordinate, target);

            DropAction dropAction = entity.GetAction<DropAction>();
            dropAction.entityToDrop = target;
            mapController.RemoveFromMap(target);
        }
    }
}