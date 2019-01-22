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
        [SerializeField] private Button m_BuildButton;
        [SerializeField] private Button m_AttackButton;

        private void OnEnable()
        {
            m_BuildButton.onClick.AddListener(OnBuildClick);
            m_AttackButton.onClick.AddListener(OnAttackClick);
        }

        private void OnDisable()
        {
            m_BuildButton.onClick.RemoveAllListeners();
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

        private void OnAttackClick()
        {
            ActionButtonClickedInput attackButtonClickedInput = new ActionButtonClickedInput()
            {
                actionType = typeof(AttackAction),
            };
            inputController.RegisterInputEvent(attackButtonClickedInput);
        }
    }
}