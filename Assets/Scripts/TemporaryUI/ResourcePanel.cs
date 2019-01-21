using ManaMist.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ResourcePanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlayerPanelPrefabReference = null;

        private CanvasGroup m_CanvasGroup = null;
        private bool m_IsVisible = false;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.M))
            {
                ToggleVisibilty();
            }
        }

        public void AddPlayerPanel(string name, Cost cost)
        {
            GameObject playerPanelInstance = Instantiate(m_PlayerPanelPrefabReference, transform);
            PlayerPanel playerPanel = playerPanelInstance.GetComponent<PlayerPanel>();

            playerPanel.cost = cost;
            playerPanel.playerName = name;
        }

        public void ToggleVisibilty()
        {
            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = m_IsVisible ? 0f : 1f;
                m_IsVisible = !m_IsVisible;
            }
        }
    }
}
