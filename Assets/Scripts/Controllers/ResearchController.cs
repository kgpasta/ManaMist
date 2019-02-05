using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.Controllers
{
    public class ResearchController : ScriptableObject
    {
        [SerializeField] private List<ResearchBase> availableResearch;

        public List<ResearchBase> GetAvailableResearch(Player player)
        {
            return availableResearch.Where(research => !player.research.Contains(research)).ToList();
        }

        public ResearchAction CreateResearch(Player player, ResearchBase researchBase)
        {
            ResearchAction researchAction = ScriptableObject.CreateInstance<ResearchAction>();
            researchAction.researchBase = researchBase;

            return researchAction;
        }
    }
}