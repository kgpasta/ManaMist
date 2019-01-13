using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class TileHighlightWidget : MonoBehaviour
    {
        [SerializeField]
        private Image m_TileImage;
        public void SetColor(Color color)
        {
            m_TileImage.color = new Color(color.r, color.g, color.b, 0.5f);
        }
    }
}