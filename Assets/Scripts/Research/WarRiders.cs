using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Research
{
    [CreateAssetMenu(menuName = "ManaMist/Research/War Riders")]
    public class WarRiders : ResearchBase
    {
        [SerializeField] private RescueAction m_RescueAction;
        [SerializeField] private DropAction m_DropAction;
        public override void PerformResearch(Player player)
        {
            foreach (Entity playerEntity in player.entities)
            {
                if (playerEntity.type.EntityClass == EntityClass.Flying ||
                    playerEntity.type.EntityClass == EntityClass.Mounted)
                {
                    playerEntity.AddAction(Instantiate(m_RescueAction));
                    playerEntity.AddAction(Instantiate(m_DropAction));
                }
            }
        }
    }
}