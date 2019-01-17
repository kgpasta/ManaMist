using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace ManaMist.UI
{
    public class ActionBarView : MonoBehaviour
    {
        [SerializeField] private SelectedState selectedState;
        [SerializeField] private EntityController entityController;
        [SerializeField] private Dispatcher m_Dispatcher;
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
            //TODO: Hard code right now to first
            Entity entity = entityController.CreateEntity(selectedState.entity.GetAction<BuildAction>().canBuildList[0]);

            PerformingActionStateData data = ScriptableObject.CreateInstance<PerformingActionStateData>();
            data.source = selectedState.entity;
            data.action = selectedState.entity.GetAction<BuildAction>();
            data.coordinate = new Coordinate(selectedState.currentlySelectedCoordinate.x + 1, selectedState.currentlySelectedCoordinate.y);
            data.target = entity;

            m_Dispatcher.Dispatch<PerformingActionState>(data);
        }
    }
}