using ManaMist.Controllers;
using ManaMist.Input;
using UnityEngine;

namespace ManaMist.UI
{
    public class HotkeyBindings : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
            {
                CycleSelectionInput cycleSelectionInput = new CycleSelectionInput();
                inputController.RegisterInputEvent(cycleSelectionInput);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                OpenResearchInput openResearchInput = new OpenResearchInput();
                inputController.RegisterInputEvent(openResearchInput);
            }
        }
    }
}