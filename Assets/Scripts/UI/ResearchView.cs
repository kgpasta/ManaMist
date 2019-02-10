using System;
using ManaMist.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class ResearchView : MonoBehaviour
    {
        [SerializeField] private ResearchBase m_Research;
        public ResearchBase Research { get { return m_Research; } }
        [SerializeField] private Sprite m_Sprite;
        public Sprite Sprite { get { return m_Sprite; } }
    }
}