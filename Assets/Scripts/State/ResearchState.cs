using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Input;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/ResearchState")]
    public class ResearchState : GameState
    {
        private List<ResearchBase> m_AvailableResearch = new List<ResearchBase>();
        public List<ResearchBase> AvailableResearch { get { return m_AvailableResearch; } }
        public List<ResearchBase> AlreadyResearched { get { return player.research; } }
        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is ResearchButtonClickedInput)
            {
                ResearchButtonClickedInput researchButtonClickedInput = inputEvent as ResearchButtonClickedInput;
                ResearchAction researchAction = researchController.CreateResearch(researchButtonClickedInput.research);
                if (researchAction.CanExecute(player))
                {
                    researchAction.Execute(player);
                }
            }
            if (inputEvent is OpenResearchInput)
            {
                dispatcher.Dispatch<IdleState>();
            }
        }

        protected override void Enter()
        {
            m_AvailableResearch = researchController.GetAvailableResearch();
        }

        protected override void Exit()
        {
            m_AvailableResearch.Clear();
        }
    }
}