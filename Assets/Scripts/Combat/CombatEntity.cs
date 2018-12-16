using ManaMist.Models;

namespace ManaMist.Combat
{
    public abstract class CombatEntity : Entity
    {
        public int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int skill { get; set; }
        public int speed { get; set; }
        public CombatEntity(string name, Cost cost) : base(name, cost)
        {
        }
    }
}