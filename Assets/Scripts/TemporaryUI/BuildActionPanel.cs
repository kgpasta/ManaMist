﻿using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

namespace ManaMist.UI
{
    public class BuildActionPanel : MonoBehaviour
    {
        [SerializeField] private EntityController m_EntityController = null;
        [SerializeField] private InputController m_InputController = null;

        [Header("UI Elements")]
        [SerializeField] private Dropdown m_EntityDropdown = null;
        [SerializeField] private Button m_BuildButton = null;

        private List<EntityType> m_CurrentEntityOptionsList = null;
        private bool m_IsDropdownUpdated = false;

        private void Awake()
        {
            m_BuildButton.onClick.AddListener(BuildButtonOnClick);
        }

        private void OnDestroy()
        {
            m_BuildButton.onClick.RemoveAllListeners();
        }

        public void Setup(List<EntityType> entityTypeList)
        {
            gameObject.SetActive(true);

            m_CurrentEntityOptionsList = entityTypeList;
            UpdateDropdownOptions();
        }

        public void Teardown()
        {
            m_EntityDropdown.ClearOptions();
            m_IsDropdownUpdated = false;

            gameObject.SetActive(false);
        }

        public void UpdateDropdownOptions()
        {
            if (!m_IsDropdownUpdated)
            {
                if (m_CurrentEntityOptionsList != null)
                {
                    foreach (EntityType type in m_CurrentEntityOptionsList)
                    {
                        m_EntityDropdown.options.Add(new OptionData(type.Name));
                    }

                    m_IsDropdownUpdated = true;
                }
            }
        }

        public void BuildButtonOnClick()
        {
            int typeIndex = m_EntityDropdown.value;
            Entity entity = m_EntityController.CreateEntity(m_CurrentEntityOptionsList[typeIndex]);

            ActionButtonClickedInput buildButtonClickedInput = new ActionButtonClickedInput()
            {
                actionType = typeof(BuildAction),
                target = entity
            };

            m_InputController.RegisterInputEvent(buildButtonClickedInput);
        }
    }
}
