using System;
using ManaMist.Combat;
using ManaMist.Utility;

namespace ManaMist.Combat
{
    public class CombatEngine
    {
        public CombatResult Battle(CombatEntity attacker, CombatEntity defender)
        {
            if (WillHit(attacker, defender))
            {
                defender.hp -= ManaMistMath.Clamp(attacker.attack - defender.defense, 0, defender.hp);

                if (defender.hp <= 0)
                {
                    return CombatResult.WIN;
                }
            }

            if (WillHit(defender, attacker))
            {
                attacker.hp -= ManaMistMath.Clamp(defender.attack - attacker.defense, 0, attacker.hp);

                if (attacker.hp <= 0)
                {
                    return CombatResult.LOSS;
                }
            }

            return CombatResult.NONE;
        }

        public bool WillHit(CombatEntity attacker, CombatEntity defender)
        {
            int hitChance = attacker.skill - defender.speed;
            return new Random().Next(0, 100) > hitChance;
        }
    }
}