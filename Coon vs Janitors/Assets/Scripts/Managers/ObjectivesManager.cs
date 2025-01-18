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

        public IReadOnlyList<IObjective> Objectives => _objectives;

        public void AddObjective(IObjective objective)
        {
            if (objective == null)
            {
                Debug.LogError("Attempted to add a null objective.");
                return;
            }

            if (!_objectives.Contains(objective))
            {
                _objectives.Add(objective);
                Debug.Log($"Objective added: {objective.Title}");
            }
        }

        public void SetObjectiveCompleted(IObjective objective)
        {
            if (objective == null)
            {
                Debug.LogError("Attempted to complete a null objective.");
                return;
            }

            if (!objective.IsCompleted)
            {
                Debug.LogError($"Objective {objective.Title} is not completed yet!");
                return;
            }

            if (objective is Objective _objective)
            {
                Debug.Log($"Objective {objective.Title} completed!");
                ObjectiveCompleted?.Invoke(_objective);
            }
            else
            {
                Debug.LogError("The provided objective is not of type Objective.");
            }
        }

        public void InitializeObjectiveManager()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("ObjectivesManager initialized.");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
