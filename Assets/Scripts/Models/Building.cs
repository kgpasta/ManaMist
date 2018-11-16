using ManaMist.Utility;

namespace ManaMist.Models
{
    public abstract class Building : Entity
    {
        public int buildTurns { get; set; }

        public Building(string name, Cost cost, int buildTurns) : base(name, cost)
        {
            this.buildTurns = buildTurns;
        }
    }
}