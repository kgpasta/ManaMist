using ManaMist.Models;

namespace ManaMist.Combat
{
    public abstract class CombatEntity : Entity
    {
        public int hp;
        public int attack;
        public int defense;
        public int skill;
        public int accuracy;
        public int speed;
        public int range;
    }
}