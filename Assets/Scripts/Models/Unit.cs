using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Unit")]
    public class Unit : Entity
    {
        public override void Init()
        {
            MoveAction moveAction = ScriptableObject.CreateInstance<MoveAction>();
            moveAction.movementRange = 3;
            moveAction.CanMove = CanMove;
            AddAction(moveAction);
        }

        public bool CanMove(Coordinate coordinate)
        {
            return true;
        }
    }
}