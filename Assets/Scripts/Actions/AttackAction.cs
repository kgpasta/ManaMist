using System.Collections.Generic;
using ManaMist.Combat;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/AttackAction")]
    public class AttackAction : Action, ISelectableTargetAction
    {
        public int attack;
        public int defense;
        public int skill;
        public int accuracy;
        public int speed;
        public int range;
        public List<AttackModifier> attackModifiers = new List<AttackModifier>();

        public int Range => range;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            Coordinate startCoordinate = mapController.GetPositionOfEntity(entity.id);
            MapTile mapTile = mapController.GetMapTileAtCoordinate(targetCoordinate);

            return base.CanExecute(player, entity, targetCoordinate, target)
            && mapTile.entities.Count > 0
            && startCoordinate.Distance(targetCoordinate) <= range;
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate, Entity target)
        {
            base.Execute(player, entity, targetCoordinate, target);

            int distance = mapController.GetPositionOfEntity(entity.id).Distance(mapController.GetPositionOfEntity(target.id));
            CombatEngine combatEngine = new CombatEngine()
            {
                attackingEntity = entity,
                defendingEntity = target,
                distance = distance,
                modifiers = attackModifiers
            };

            CombatResult result = combatEngine.Battle();
            if (result == CombatResult.LOSS)
            {
                player.RemoveEntity(entity);
                mapController.RemoveFromMap(entity);
            }
            else if (result == CombatResult.WIN)
            {
                mapController.RemoveFromMap(target);
            }
        }
    }
}