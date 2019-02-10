using System.Collections.Generic;
using System.Linq;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.UI;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/Research Controller")]
    public class ResearchController : ScriptableObject
    {
        [SerializeField] private List<ResearchView> availableResearch;

        public List<ResearchBase> GetAvailableResearch()
        {
            return availableResearch.Select(researchView => researchView.Research).ToList();
        }

        public ResearchAction CreateResearch(ResearchBase research)
        {
            ResearchAction researchAction = ScriptableObject.CreateInstance<ResearchAction>();
            researchAction.research = research;

            return researchAction;
        }

        public GameObject GetResearchPrefab(ResearchBase research)
        {
            return availableResearch.Find(researchView => researchView.Research.name == research.name).gameObject;
        }
    }
}