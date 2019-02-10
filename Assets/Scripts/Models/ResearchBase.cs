using System.Collections.Generic;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.Models
{
    public abstract class ResearchBase : ScriptableObject
    {
        public string displayName;
        public Cost cost;
        public List<ResearchBase> prerequesites = new List<ResearchBase>();

        public abstract void PerformResearch(Player player);
    }
}