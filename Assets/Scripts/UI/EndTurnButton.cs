using ManaMist.Controllers;
using ManaMist.Input;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class EndTurnButton : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private Button m_Button;

        private void OnEnable()
        {
            m_Button.onClick.AddListener(EndTurn);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveAllListeners();
        }

        private void EndTurn()
        {
            EndOfTurnInput endOfTurnInput = new EndOfTurnInput();
            inputController.RegisterInputEvent(endOfTurnInput);
        }
    }
}