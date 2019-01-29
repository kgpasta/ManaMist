using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpBarContainer : MonoBehaviour
{
    public float currentHp;
    public float maxHp;

    [Header("UI Elements")]
    [SerializeField] private RectTransform m_HpImageTransform;
    [SerializeField] private RectTransform m_HpFillImageTransform;
    [SerializeField] private TextMeshProUGUI m_CurrentHpText;
    [SerializeField] private TextMeshProUGUI m_MaxHpText;

    private void OnGUI()
    {
        m_CurrentHpText.text = currentHp.ToString();
        m_MaxHpText.text = maxHp.ToString();

        if (maxHp > 0 && currentHp <= maxHp)
        {
            float hpWidth = (currentHp / maxHp) * m_HpImageTransform.sizeDelta.x;

            m_HpFillImageTransform.sizeDelta = new Vector2(hpWidth, m_HpFillImageTransform.sizeDelta.y);
        }
    }
}
