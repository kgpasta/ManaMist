using ManaMist.Controllers;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.AI
{
    [CreateAssetMenu(menuName = "ManaMist/Behavior/AIBehavior")]
    public class AIBehavior : Behavior
    {
        public TurnController turnController;
        public MapController mapController;
        public EntityController entityController;

        public override void OnTurnStart()
        {
            turnController.EndTurn();
        }

    }
}