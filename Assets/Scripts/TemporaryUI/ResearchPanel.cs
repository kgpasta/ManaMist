using System;
using System.Collections.Generic;
using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.State;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class ResearchPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject m_LevelOne;
        [SerializeField] private GameObject m_LevelTwo;
        [SerializeField] private GameObject m_LevelThree;

        [Header("Prefab References")]
        [SerializeField] private GameObject m_ResearchButtonPrefab;

        [Header("Controllers")]
        [SerializeField] private InputController m_InputController;

        [Header("State")]
        [SerializeField] private ResearchState m_ResearchState;
        private CanvasGroup m_CanvasGroup;

        private void Awake()
        {
            m_CanvasGroup = this.GetComponent<CanvasGroup>();
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
            m_CanvasGroup.alpha = 1;
            m_CanvasGroup.interactable = true;
            m_CanvasGroup.blocksRaycasts = true;
        }

        private void OnExit(object sender, EventArgs e)
        {
            m_CanvasGroup.alpha = 0;
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
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
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
            foreach (Transform child in m_LevelTwo.transform)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
            foreach (Transform child in m_LevelThree.transform)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
            gameObject.SetActive(false);
        }

        private void CreateResearchButton(Research research, Transform parent)
        {
            GameObject newButton = Instantiate(m_ResearchButtonPrefab, parent);
            newButton.GetComponentInChildren<Text>().text = research.name;
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                ResearchButtonClickedInput researchButtonClickedInput = new ResearchButtonClickedInput()
                {
                    research = research
                };
                m_InputController.RegisterInputEvent(researchButtonClickedInput);
            });
        }

    }
}