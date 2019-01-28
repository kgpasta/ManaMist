using ManaMist.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EntityInspectorPanel : MonoBehaviour
{
    public Entity entity = null;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI m_NameText = null;
    [SerializeField] private TextMeshProUGUI m_TypeText = null;
    [SerializeField] private HpBarContainer m_HpBarContainer = null;
    [SerializeField] private Button m_CloseButton = null;

    private void Awake()
    {
        m_CloseButton.onClick.AddListener(HidePanel);
    }

    private void OnDestroy()
    {
        m_CloseButton.onClick.RemoveAllListeners();
    }

    private void OnGUI()
    {
        if (entity != null)
        {
            m_NameText.text = entity.name;
            m_TypeText.text = entity.type.ToString();
            m_HpBarContainer.currentHp = entity.hp;
            m_HpBarContainer.maxHp = entity.MaxHp;
        }
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
