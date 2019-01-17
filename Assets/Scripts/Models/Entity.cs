using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    public abstract class Entity : ScriptableObject
    {
        public string id;
        public EntityType type;
        public Cost cost;
        public int actionPoints;
        public List<Action> actions;

        private void Awake()
        {
            id = System.Guid.NewGuid().ToString();
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public T GetAction<T>() where T : Action
        {
            return actions.Find(action => action is T) as T;
        }
    }
}