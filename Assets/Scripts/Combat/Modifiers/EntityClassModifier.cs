using System.Collections.Generic;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.Combat
{
    [CreateAssetMenu(menuName = "ManaMist/Modifiers/EntityClassAttackModifier")]
    public class EntityClassAttackModifier : AttackModifier
    {
        public int HitMultiplier;
        public int DamageMultiplier;

        public EntityClass entityClass;

        public override int CalculateDamageModifier(Entity attacker, Entity defender, int baseDamage)
        {
            return entityClass.Equals(defender.Type.EntityClass) ? baseDamage * DamageMultiplier : baseDamage;
        }

        public override int WillHitModifier(Entity attacker, Entity defender, int baseWillHit)
        {
            return entityClass.Equals(attacker.Type.EntityClass) ? baseWillHit * HitMultiplier : baseWillHit;
        }
    }
}