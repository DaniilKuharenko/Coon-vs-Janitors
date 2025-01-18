using UnityEngine;

namespace Raccons_House_Games
{
    public class GoToObjectiveTest : Objective
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _completionDistance = 0.25f;
        private bool _isObjectiveReached = false;

        protected override void StartObjective()
        {
            base.StartObjective();
            if (_playerTransform == null)
            {
                Debug.LogError("Player Transform is not assigned!");
            }
        }

        protected override bool IsObjectiveCompleted()
        {
            if (_playerTransform == null)
            {
                Debug.LogError("Player Transform is null in IsObjectiveCompleted!");
                return false;
            }

            float distance = Vector3.Distance(_playerTransform.position, transform.position);
            
            // If the distance is less than the specified threshold and the task has not yet been completed
            if (distance < _completionDistance && !_isObjectiveReached)
            {
                _isObjectiveReached = true;
                Debug.LogError("Objective completed!");
                return true;
            }

            return false;
        }
    }
}
