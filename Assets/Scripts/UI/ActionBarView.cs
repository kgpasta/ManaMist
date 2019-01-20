using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private EntityController entityController;
        [SerializeField] private InputController inputController;
        [SerializeField] private Button m_Button;

        private void OnEnable()
        {
            m_Button.onClick.AddListener(OnBuildClick);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveAllListeners();
        }

        private void OnBuildClick()
        {
            //TODO: Hard code right now to mine
            Entity entity = entityController.CreateEntity(EntityType.Mine);
            ActionButtonClickedInput buildButtonClickedInput = new ActionButtonClickedInput()
            {
                actionType = typeof(BuildAction),
                target = entity
            };
            inputController.RegisterInputEvent(buildButtonClickedInput);
        }
    }
}