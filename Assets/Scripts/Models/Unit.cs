using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    public abstract class Unit : Entity
    {
        public int movementRange;

        public override void Init()
        {
            MoveAction moveAction = ScriptableObject.CreateInstance<MoveAction>();
            moveAction.CanMove = CanMove;
            AddAction(moveAction);
        }

        public bool CanMove(Coordinate start, Coordinate end)
        {
            return start.Distance(end) <= movementRange;
        }
    }
}