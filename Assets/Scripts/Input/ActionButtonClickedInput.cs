using System;
using ManaMist.Models;

namespace ManaMist.Input
{
    public class ActionButtonClickedInput : InputEvent
    {
        public Type actionType;
        public Entity target;
    }
}