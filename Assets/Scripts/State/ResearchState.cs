using System.Collections.Generic;
using ManaMist.Input;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/ResearchState")]
    public class ResearchState : GameState
    {
        private List<Research> m_AvailableResearch = new List<Research>();
        public List<Research> AvailableResearch { get { return m_AvailableResearch; } }
        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is OpenResearchInput)
            {
                dispatcher.Dispatch<IdleState>();
            }
        }

        protected override void Enter()
        {
            m_AvailableResearch = researchController.GetAvailableResearch(player);
        }

        protected override void Exit()
        {
            m_AvailableResearch.Clear();
        }
    }
}