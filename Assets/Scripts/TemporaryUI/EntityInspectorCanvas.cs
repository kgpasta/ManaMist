using ManaMist.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EntityInspectorCanvas : MonoBehaviour
{
    public Entity entity = null;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI m_NameText = null;
    [SerializeField] private TextMeshProUGUI m_TypeText = null;

    private void OnGUI()
    {
        if (entity != null)
        {
            m_NameText.text = entity.name;
            m_TypeText.text = entity.type.ToString();
        }
    }
}
