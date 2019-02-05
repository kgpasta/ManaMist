using System;
using System.Collections.Generic;
using ManaMist.Models;
using ManaMist.State;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class ResearchPanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_LevelOne;
        [SerializeField] private GameObject m_LevelTwo;
        [SerializeField] private GameObject m_LevelThree;
        [SerializeField] private GameObject m_ResearchButtonPrefab;
        [SerializeField] private ResearchState m_ResearchState;

        private void Awake()
        {
            m_ResearchState.OnEnter += OnEnter;
            m_ResearchState.OnExit += OnExit;
        }

        private void OnDestroy()
        {
            m_ResearchState.OnEnter -= OnEnter;
            m_ResearchState.OnExit -= OnExit;
        }

        private void OnEnter(object sender, EventArgs e)
        {
            Setup(m_ResearchState.AvailableResearch);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Clear();
        }

        private void Setup(List<Research> researchs)
        {
            gameObject.SetActive(true);

            foreach (Research research in researchs)
            {
                if (research.prerequesites.Count == 0)
                {
                    CreateResearchButton(research, m_LevelOne.transform);
                }
                else if (research.prerequesites.Count < 2)
                {
                    CreateResearchButton(research, m_LevelTwo.transform);
                }
                else
                {
                    CreateResearchButton(research, m_LevelThree.transform);
                }
            }
        }

        private void Clear()
        {
            foreach (Transform child in m_LevelOne.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in m_LevelTwo.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in m_LevelThree.transform)
            {
                Destroy(child.gameObject);
            }
            gameObject.SetActive(false);
        }

        private void CreateResearchButton(Research research, Transform parent)
        {
            GameObject newButton = Instantiate(m_ResearchButtonPrefab, parent);
            newButton.GetComponentInChildren<Text>().text = research.name;
        }

    }
}