using ManaMist.Models;
using ManaMist.State;
using ManaMist.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private SelectedState m_SelectedState = null;

        [Header("UI Elements")]
        [SerializeField] private PlayerPanel m_PlayerOnePanel = null;
        [SerializeField] private PlayerPanel m_PlayerTwoPanel = null;
        [SerializeField] private BuildActionPanel m_BuildActionPanel = null;
        [SerializeField] private AttackActionPanel m_AttackActionPanel = null;

        private CanvasGroup m_CanvasGroup = null;
        private bool m_IsVisible = false;

        private List<Action> m_Actions = null;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();

            m_SelectedState.OnEnter += OnEnter;
            m_SelectedState.OnExit += OnExit;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.M))
            {
                ToggleVisibilty();
            }
        }

        private void OnDestroy()
        {
            m_SelectedState.OnEnter -= OnEnter;
            m_SelectedState.OnExit -= OnExit;
        }

        private void OnEnter(object sender, System.EventArgs e)
        {
            m_Actions = m_SelectedState.CurrentlySelectedEntity.actions;

            

            TogglePanels();
        }

        private void OnExit(object sender, System.EventArgs e)
        {
            ResetPanels();
        }

        public void ToggleVisibilty()
        {
            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = m_IsVisible ? 0f : 1f;
                m_IsVisible = !m_IsVisible;
            }
        }

        private void TogglePanels()
        {
            foreach (Action action in m_Actions)
            {
                if (action is BuildAction)
                {
                    m_BuildActionPanel.Setup(m_SelectedState.CurrentlySelectedEntity.GetAction<BuildAction>()?.canBuildList);
                }
                if (action is AttackAction)
                {
                    m_AttackActionPanel.gameObject.SetActive(true);
                }
            }
        }

        private void ResetPanels()
        {
            m_BuildActionPanel.Teardown();
            m_AttackActionPanel.gameObject.SetActive(false);
        }
    }
}
