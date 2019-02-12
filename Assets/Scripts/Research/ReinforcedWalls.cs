using ManaMist.Models;
using ManaMist.Players;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Research
{
    [CreateAssetMenu(menuName = "ManaMist/Research/Reinforced Walls")]
    public class ReinforcedWalls : ResearchBase
    {
        [SerializeField] private double scaleFactor;

        public override void PerformResearch(Player player)
        {
            foreach (Entity playerEntity in player.entities)
            {
                if (playerEntity.Type.EntityClass == EntityClass.Building)
                {
                    playerEntity.MaxHp = (int)System.Math.Round(playerEntity.MaxHp * scaleFactor);
                    playerEntity.Hp = (int)System.Math.Round(playerEntity.Hp * scaleFactor);
                }
            }
        }
    }
}