using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.UI;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/ResearchController")]
    public class ResearchController : ScriptableObject
    {
        [SerializeField] private List<ResearchView> availableResearch;

        public List<Research> GetAvailableResearch(Player player)
        {
            return availableResearch.Where(researchView => !player.research.Contains(researchView.Research))
                                    .Select(researchView => researchView.Research)
                                    .ToList();
        }

        public ResearchAction CreateResearch(Research research)
        {
            ResearchAction researchAction = ScriptableObject.CreateInstance<ResearchAction>();
            researchAction.research = research;

            return researchAction;
        }

        public GameObject GetResearchPrefab(Research research)
        {
            return availableResearch.Find(researchView => researchView.Research.name == research.name).gameObject;
        }
    }
}