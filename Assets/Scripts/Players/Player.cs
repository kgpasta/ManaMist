using System;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.State;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Players
{
    [CreateAssetMenu(menuName = "ManaMist/Player")]
    public class Player : ScriptableObject
    {
        public int id;
        private IPlayerState m_State;
        public IPlayerState state
        {
            get { return m_State; }
            set
            {
                m_State = value;
                state.Update();
                OnStateChange?.Invoke(this, value);
            }
        }
        public List<Entity> entities = new List<Entity>();
        public Entity selectedEntity = null;
        public Cost resources;

        public event EventHandler<IPlayerState> OnStateChange;

        public void InitializeTurn()
        {
            IncrementResources();

            Debug.Log("Player " + id + " has " + resources.ToString());
        }

        private void IncrementResources()
        {
            foreach (Entity entity in entities)
            {
                HarvestAction action = entity.GetAction<HarvestAction>();

                if (action != null)
                {
                    resources.Increment(action.harvestAmount);
                }

            }
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public Entity GetEntity(string id)
        {
            return entities.Find(entity => entity.id == id);
        }

        public void SelectEntity(string id, Coordinate coordinate)
        {
            Entity entity = entities.Find(e => e.id == id);
            if (entity != null)
            {
                selectedEntity = entity;
                state = new SelectedState() { entity = entity, coordinate = coordinate };
            }
        }

        private void OnDisable()
        {
            state = new WaitingState();
            entities = new List<Entity>();
            selectedEntity = null;
        }

    }
}
