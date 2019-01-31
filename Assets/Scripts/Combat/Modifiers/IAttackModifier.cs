using ManaMist.Actions;
using ManaMist.Models;

namespace ManaMist.Combat
{
    public interface IAttackModifier
    {
        int WillHitModifier(Entity attacker, Entity defender, int baseWillHit);
        int CalculateDamageModifier(Entity attacker, Entity defender, int baseDamage);
    }
}