using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/HealAction")]
    public class HealAction : Action, ISelectableTargetAction
    {
        public int healAmount;
        public int range;
        public int Range => range;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);
            return base.CanExecute(player, entity, targetCoordinate, target)
            && mapTile.entities.Count > 0;
        }

        public bool CanPerform(MapTile mapTile)
        {
            return mapTile.entities.Count > 0;
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            base.Execute(player, entity, targetCoordinate, target);
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);

            target.hp = ManaMistMath.Clamp(target.hp + healAmount, 0, target.MaxHp);
        }
    }
}