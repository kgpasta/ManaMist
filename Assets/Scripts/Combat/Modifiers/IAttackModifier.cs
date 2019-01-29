using ManaMist.Actions;

namespace ManaMist.Combat
{
    public interface IAttackModifier
    {
        int WillHitModifier(AttackAction attacker, AttackAction defender, int baseWillHit);
        int CalculateDamageModifier(AttackAction attacker, AttackAction defender, int baseDamage);
    }
}