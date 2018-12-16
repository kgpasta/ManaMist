using System;
using ManaMist.Combat;
using ManaMist.Utility;

namespace ManaMist.Combat
{
    public class CombatEngine
    {
        public CombatEntity attacker;
        public CombatEntity defender;
        public int distance;

        public CombatResult Battle()
        {
            if (WillHit(attacker, defender, distance))
            {
                int damageModifier = WillCrit(attacker) ? 2 : 1;
                int damage = CalculateDamage(attacker, defender, damageModifier);
                defender.hp -= ManaMistMath.Clamp(damage, 0, defender.hp);

                if (defender.hp <= 0)
                {
                    return CombatResult.WIN;
                }
            }

            if (WillHit(defender, attacker, distance))
            {
                int damageModifier = WillCrit(defender) ? 2 : 1;
                int damage = CalculateDamage(defender, attacker, damageModifier);
                attacker.hp -= ManaMistMath.Clamp(damage, 0, attacker.hp);

                if (attacker.hp <= 0)
                {
                    return CombatResult.LOSS;
                }
            }

            return CombatResult.NONE;
        }

        private bool WillHit(CombatEntity attacker, CombatEntity defender, int distance)
        {
            bool inRange = attacker.range >= distance;
            int hitChance = attacker.accuracy - defender.speed;
            return inRange && new Random().Next(0, 100) > hitChance;
        }

        private bool WillCrit(CombatEntity attacker)
        {
            return new Random().Next(0, 100) > attacker.skill;
        }

        private int CalculateDamage(CombatEntity attacker, CombatEntity defender, int damageModifier)
        {
            return (attacker.attack - defender.defense) * damageModifier;
        }
    }

    public enum CombatResult
    {
        WIN, LOSS, NONE
    }
}