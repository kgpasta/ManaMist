using ManaMist.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackActionPanel : MonoBehaviour
{
    [SerializeField] private EntityController m_EntityController = null;
    [SerializeField] private InputController m_InputController = null;

    [Header("UI Elements")]
    [SerializeField] private Button m_AttackButton = null;

    private void Awake()
    {
        m_AttackButton.onClick.AddListener(AttackButtonOnClick);
    }

    private void OnDestroy()
    {
        m_AttackButton.onClick.RemoveAllListeners();
    }

    private void AttackButtonOnClick()
    {
        
    }
}
