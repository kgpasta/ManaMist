using System;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Players
{
    [CreateAssetMenu(menuName = "ManaMist/Player")]
    public class Player : ScriptableObject
    {
        public int id { get; set; }
        public Phase currentPhase = Phase.WAITING;
        public Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
        public Entity selectedEntity { get; set; } = null;
        public Cost resources { get; set; } = new Cost();

        private List<Phase> m_Phases = new List<Phase>();
        private int m_PhaseIndex = 0;

        public Player(int id, TurnController turnController)
        {
            this.id = id;
            turnController.OnTurnStart += InitializeTurn;

            m_Phases.Add(Phase.ACTIVE);
        }

        private void InitializeTurn(object sender, TurnEventArgs args)
        {
            if (args.player == id)
            {
                currentPhase = m_Phases[m_PhaseIndex];

                IncrementResources();

                Console.WriteLine("Player " + id + " has " + resources);
            }
        }

        private void IncrementResources()
        {
            foreach (Entity entity in entities.Values)
            {
                HarvestAction action = entity.GetAction<HarvestAction>();

                if (action != null)
                {
                    resources.Increment(action.harvestAmount);
                }

            }
        }

        private void IncrementPhase()
        {
            m_PhaseIndex++;
            if (m_Phases.Count > m_PhaseIndex)
            {
                currentPhase = m_Phases[m_PhaseIndex];
            }
            else
            {
                m_PhaseIndex = 0;
                currentPhase = Phase.WAITING;
            }
        }

        private void DecrementPhase()
        {
            m_PhaseIndex--;
            if (m_PhaseIndex >= 0)
            {
                currentPhase = m_Phases[m_PhaseIndex];
            }
            else
            {
                m_PhaseIndex = 1;
                currentPhase = Phase.ACTIVE;
            }
        }

        public void AddEntity(Entity entity)
        {
            entities[entity.id] = entity;
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity.id);
        }

        public Entity GetEntity(string id)
        {
            return entities.ContainsKey(id) ? entities[id] : null;
        }

        public void SelectEntity(string id)
        {
            if (entities.ContainsKey(id))
            {
                selectedEntity = entities[id];
            }
        }

    }
}
