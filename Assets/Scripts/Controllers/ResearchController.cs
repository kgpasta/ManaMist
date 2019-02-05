using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/ResearchController")]
    public class ResearchController : ScriptableObject
    {
        [SerializeField] private List<Research> availableResearch;

        public List<Research> GetAvailableResearch(Player player)
        {
            return availableResearch.Where(research => !player.research.Contains(research)).ToList();
        }

        public ResearchAction CreateResearch(Player player, Research research)
        {
            ResearchAction researchAction = ScriptableObject.CreateInstance<ResearchAction>();
            researchAction.research = research;

            return researchAction;
        }
    }
}