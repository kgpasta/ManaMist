using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Research
{
    [CreateAssetMenu(menuName = "ManaMist/Research/Twin Cities")]
    public class TwinCities : ResearchBase
    {
        [SerializeField] private EntityType m_EntityType;

        public override void PerformResearch(Player player)
        {
            Entity target = player.entities.Find(entity => entity.Type.Equals(m_EntityType));

            LimitedEntityAction limitedEntityAction = target.GetAction<LimitedEntityAction>();
            limitedEntityAction.EntitiesAllowed++;
        }
    }
}