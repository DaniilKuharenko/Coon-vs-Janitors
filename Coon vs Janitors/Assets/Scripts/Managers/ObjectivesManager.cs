using System;
using System.Collections.Generic;
using UnityEngine;

namespace Raccons_House_Games
{
    public class ObjectivesManager : MonoBehaviour
    {
        private readonly List<IObjective> _objectives = new();

        public event Action<Objective> ObjectiveCompleted;
        public static ObjectivesManager Instance { get; private set; }

        // This is Objectives list. Need this to iterate over all Objectives and display them in UI
        public IReadOnlyList<IObjective> Objectives => _objectives;

        public void AddObjective(IObjective objective)
        {
            // Sanity check
            if(objective == null)
            {
                throw new NullReferenceException(objective?.ToString() ?? "Unknown objective is null");
            }

            if (!_objectives.Contains(objective))
            {
                _objectives.Add(objective);
            }
        }
        
        public void SetObjectiveCompleted(IObjective objective)
        {
            // Sanity check
            if(objective == null)
            {
                throw new NullReferenceException(objective?.ToString() ?? "Unknown objective is null");
            }

            if(!objective.IsCompleted)
            {
                throw new Exception($"Objective {objective} is not completed yet!");
            }

            if (objective is Objective _objective)
            {
                ObjectiveCompleted?.Invoke(_objective);
            }
            else
            {
                throw new InvalidCastException("The provided objective is not of type Objective.");
            }

        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}