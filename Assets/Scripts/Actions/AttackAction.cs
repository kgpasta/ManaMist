using ManaMist.Combat;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class AttackAction : Action
    {
        public CanAttackFunction CanAttack;

        public delegate bool CanAttackFunction(Coordinate currentCoordinate, Coordinate targetCoordinate);

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate)
        {
            Coordinate startCoordinate = mapController.GetPositionOfEntity(entity.id);
            return base.CanExecute(player, entity, targetCoordinate) && CanAttack(startCoordinate, targetCoordinate);
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate)
        {
            base.Execute(player, entity, targetCoordinate);

            Entity target = mapController.GetMapTileAtCoordinate(targetCoordinate).entities[0];

            int distance = mapController.GetPositionOfEntity(entity.id).Distance(mapController.GetPositionOfEntity(target.id));
            CombatEngine combatEngine = new CombatEngine()
            {
                attacker = entity as CombatEntity,
                defender = target as CombatEntity,
                distance = distance
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