using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ManaMist.Models;

namespace ManaMist.UI
{
    public class PlayerPanel : MonoBehaviour
    {
        public Cost cost;
        public string playerName
        {
            set { m_PlayerName.text = value; }
        }

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI m_PlayerName = null;
        [SerializeField] private TextMeshProUGUI m_FoodText = null;
        [SerializeField] private TextMeshProUGUI m_MetalText = null;
        [SerializeField] private TextMeshProUGUI m_ManaText = null;

        private void Update()
        {
            m_FoodText.text = cost.food.ToString();
            m_MetalText.text = cost.metal.ToString();
            m_ManaText.text = cost.mana.ToString();
        }
    }
}

