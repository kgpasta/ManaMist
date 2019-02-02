using ManaMist.Controllers;
using ManaMist.Input;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ManaMist.UI
{
    public class EndTurnActionPanel : MonoBehaviour
    {
        [SerializeField] private TurnController m_TurnController = null;
        [SerializeField] private InputController m_InputController = null;

        [Header("UI Elements")]
        [SerializeField] private Button m_EndTurnButton = null;
        [SerializeField] private TextMeshProUGUI m_CurrentPlayerText = null;

        private void Awake()
        {
            m_EndTurnButton.onClick.AddListener(EndTurnButtonOnClick);
        }

        private void OnGUI()
        {
            if (m_TurnController?.currentPlayer != null)
            {
                m_CurrentPlayerText.text = m_TurnController.currentPlayer.name;
            }
        }

        private void OnDestroy()
        {
            m_EndTurnButton.onClick.RemoveAllListeners();
        }

        private void EndTurnButtonOnClick()
        {
            EndOfTurnInput endOfTurnInput = new EndOfTurnInput();
            m_InputController.RegisterInputEvent(endOfTurnInput);
        }
    }
}