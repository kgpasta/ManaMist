using ManaMist.Actions;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.Combat
{
    public abstract class AttackModifier : ScriptableObject
    {
        public abstract int WillHitModifier(Entity attacker, Entity defender, int baseWillHit);
        public abstract int CalculateDamageModifier(Entity attacker, Entity defender, int baseDamage);
    }
}