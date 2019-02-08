using System;
using ManaMist.Input;
using ManaMist.State;
using UnityEngine;

namespace ManaMist.Controllers
{
    [CreateAssetMenu(menuName = "ManaMist/Input Controller")]
    public class InputController : ScriptableObject
    {
        public event EventHandler<InputEvent> OnInputEvent;
        public void RegisterInputEvent(InputEvent inputEvent)
        {
            OnInputEvent?.Invoke(this, inputEvent);
        }

        public void Test(string test)
        {
            Debug.Log(test);
        }
    }
}