using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Research
{
    [CreateAssetMenu(menuName = "ManaMist/Research/Foundational Roots")]
    public class FoundationalRootsResearch : ResearchBase
    {
        [SerializeField] private MoveAction moveAction;

        public override void PerformResearch(Player player)
        {
            foreach (Entity playerEntity in player.entities)
            {
                if (playerEntity.type.EntityClass == EntityClass.Building)
                {
                    playerEntity.AddAction(moveAction);
                }
            }
        }
    }
}