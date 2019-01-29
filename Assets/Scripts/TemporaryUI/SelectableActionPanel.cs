using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableActionPanel : MonoBehaviour
{
    private Type actionType;
    [SerializeField] private InputController m_InputController = null;

    [Header("UI Elements")]
    [SerializeField] private Button m_Button = null;

    public void Init(Type actionType)
    {
        this.actionType = actionType;
        this.m_Button.GetComponentInChildren<Text>().text = actionType.Name.Replace("Action", "").ToUpperInvariant();
    }

    private void Awake()
    {
        m_Button.onClick.AddListener(AttackButtonOnClick);
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    private void AttackButtonOnClick()
    {
        ActionButtonClickedInput actionClickedInput = new ActionButtonClickedInput()
        {
            actionType = this.actionType,
        };
        m_InputController.RegisterInputEvent(actionClickedInput);
    }
}
