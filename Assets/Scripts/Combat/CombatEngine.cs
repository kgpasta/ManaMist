using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Combat;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Combat
{
    public class CombatEngine
    {
        public Entity attackingEntity;
        public Entity defendingEntity;
        public int distance;
        public List<AttackModifier> modifiers = new List<AttackModifier>();

        public CombatResult Battle()
        {
            AttackAction attacker = attackingEntity.GetAction<AttackAction>();
            AttackAction defender = defendingEntity.GetAction<AttackAction>();

            if (WillHit(attacker, defender, distance))
            {
                int damageModifier = WillCrit(attacker) ? 2 : 1;
                int damage = CalculateDamage(attacker, defendingEntity, damageModifier);
                defendingEntity.Hp -= ManaMistMath.Clamp(damage, 0, defendingEntity.Hp);
                Debug.Log(attackingEntity.name + " hit for " + damage);

                if (defendingEntity.Hp <= 0)
                {
                    return CombatResult.WIN;
                }
            }

            if (WillHit(defender, attacker, distance))
            {
                int damageModifier = WillCrit(defender) ? 2 : 1;
                int damage = CalculateDamage(defender, attackingEntity, damageModifier);
                attackingEntity.Hp -= ManaMistMath.Clamp(damage, 0, attackingEntity.Hp);
                Debug.Log(defendingEntity.name + " hit for " + damage);

                if (attackingEntity.Hp <= 0)
                {
                    return CombatResult.LOSS;
                }
            }

            return CombatResult.NONE;
        }

        private bool WillHit(AttackAction attacker, AttackAction defender, int distance)
        {
            bool inRange = attacker.range >= distance;
            int defenderSpeed = defender != null ? defender.speed : 0;
            int hitChance = attacker.accuracy - defenderSpeed;
            foreach (AttackModifier modifier in modifiers)
            {
                hitChance = modifier.WillHitModifier(attackingEntity, defendingEntity, hitChance);
            }
            int roll = new System.Random().Next(0, 100);

            Debug.Log("Attacker rolled a " + roll + " hit chance is " + hitChance);
            return inRange && roll < hitChance;
        }

        private bool WillCrit(AttackAction attacker)
        {
            return new System.Random().Next(0, 100) < attacker.skill;
        }

        private int CalculateDamage(AttackAction attacker, Entity defender, int damageModifier)
        {
            int defenderDefense = defender != null ? defender.Defense : 0;
            int baseDamage = (attacker.attack - defenderDefense) * damageModifier;
            foreach (AttackModifier modifier in modifiers)
            {
                baseDamage = modifier.CalculateDamageModifier(attackingEntity, defendingEntity, baseDamage);
            }
            return baseDamage;
        }
    }

    public enum CombatResult
    {
        WIN, LOSS, NONE
    }
}