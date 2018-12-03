using ManaMist.Actions;
using ManaMist.Utility;

namespace ManaMist.Models
{
    public abstract class Unit : Entity
    {
        public int movementRange { get; set; }

        public bool CanMove(Coordinate start, Coordinate end)
        {
            return start.Distance(end) <= movementRange;
        }
    }
}