using System.Collections.Generic;
using System.Linq;
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
        public int hp;
        public int maxHp;

        private void Awake()
        {
            id = System.Guid.NewGuid().ToString();
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public Action GetAction(System.Type type)
        {
            return actions.Find(action => action.GetType() == type);
        }

        public T GetAction<T>() where T : Action
        {
            return actions.Find(action => action is T) as T;
        }

        public List<T> GetActions<T>()
        {
            return actions.FindAll(action => action is T).Cast<T>().ToList();
        }

        public void ReduceActionPoints(int points)
        {
            ManaMistMath.Clamp(actionPoints -= points, 0, 100);
        }
    }
}