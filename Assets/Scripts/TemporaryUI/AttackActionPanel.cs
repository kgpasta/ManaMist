using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackActionPanel : MonoBehaviour
{
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
        ActionButtonClickedInput attackButtonClickedInput = new ActionButtonClickedInput()
        {
            actionType = typeof(AttackAction),
        };
        m_InputController.RegisterInputEvent(attackButtonClickedInput);
    }
}
