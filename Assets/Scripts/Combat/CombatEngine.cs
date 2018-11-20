using ManaMist.Combat;
using ManaMist.Utility;

namespace ManaMist.Combat
{
    public class CombatEngine
    {
        public CombatResult Battle(CombatEntity attacker, CombatEntity defender)
        {
            defender.hp -= ManaMistMath.Clamp(attacker.attack - defender.defense, 0, defender.hp);

            if (defender.hp <= 0)
            {
                return CombatResult.WIN;
            }

            attacker.hp -= ManaMistMath.Clamp(defender.attack - attacker.defense, 0, attacker.hp);

            if (attacker.hp <= 0)
            {
                return CombatResult.LOSS;
            }

            return CombatResult.NONE;
        }
    }
}