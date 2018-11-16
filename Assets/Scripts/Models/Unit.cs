using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public abstract class Unit : Entity
    {
        public int movementRange { get; set; }

        public Unit(string name, Cost cost, int movementRange) : base(name, cost)
        {
            this.movementRange = movementRange;

            MoveAction moveAction = new MoveAction(CanMove);
            this.AddAction(moveAction);
        }

        public bool CanMove(Coordinate start, Coordinate end)
        {
            return start.Distance(end) <= movementRange;
        }
    }
}