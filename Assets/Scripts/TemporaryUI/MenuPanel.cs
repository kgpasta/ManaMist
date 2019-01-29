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

        [Header("Prefab References")]
        [SerializeField] private GameObject m_SelectableActionPanelPrefab = null;

        [Header("UI Elements")]
        [SerializeField] private BuildActionPanel m_BuildActionPanel = null;
        [SerializeField] private GameObject m_ActionPanelContainer = null;
        [SerializeField] private List<GameObject> m_Panels = new List<GameObject>();

        private CanvasGroup m_CanvasGroup = null;
        private bool m_IsVisible = true;

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
                else if (action is ISelectableTargetAction && !(action is MoveAction))
                {
                    GameObject selectableActionPanel = Instantiate(m_SelectableActionPanelPrefab, m_ActionPanelContainer.transform);
                    selectableActionPanel.transform.SetAsFirstSibling();
                    selectableActionPanel.GetComponent<SelectableActionPanel>().Init(action.GetType());
                    m_Panels.Add(selectableActionPanel);
                    selectableActionPanel.SetActive(true);
                }
            }
        }

        private void ResetPanels()
        {
            m_BuildActionPanel.Teardown();
            foreach (GameObject panel in m_Panels)
            {
                Destroy(panel);
            }
            m_Panels.Clear();
        }
    }
}
