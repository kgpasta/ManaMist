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

        public delegate bool CanAttackFunction(Coordinate currentCoordinate, Coordinate targetCoordinate, Entity target);

        public override bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            Coordinate startCoordinate = mapController.GetPositionOfEntity(entity.id);
            return CanAttack(startCoordinate, coordinate, target);
        }

        public override void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            base.Execute(mapController, player, entity, coordinate, target);

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