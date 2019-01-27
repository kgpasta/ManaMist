using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/HarvestAction")]
    public class HarvestAction : Action, IBuildConstraint, ITurnStartAction
    {
        public Resource resource;
        public Cost harvestAmount;

        [SerializeField] private int m_TurnLeft = 100000; //Essentially infinite, since this never expires
        public int TurnsLeft { get => m_TurnLeft; set => m_TurnLeft = value; }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate = null, Entity target = null)
        {
            player.resources.Increment(harvestAmount);
        }

        public bool CanBuild(MapTile mapTile)
        {
            return resource == mapTile.resource;
        }
    }
}