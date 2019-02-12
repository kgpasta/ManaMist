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

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            base.Execute(player, entity, targetCoordinate, target);

            target.Hp = ManaMistMath.Clamp(target.Hp + healAmount, 0, target.MaxHp);
        }
    }
}