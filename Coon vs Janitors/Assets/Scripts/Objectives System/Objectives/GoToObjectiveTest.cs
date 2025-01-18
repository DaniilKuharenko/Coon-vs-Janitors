using UnityEngine;

namespace Raccons_House_Games
{
    public class GoToObjectiveTest : Objective
    {
        [SerializeField] private Transform _playerTransform;

        protected override bool IsObjectiveCompleted()
        {
            return Vector3.Distance(_playerTransform.position, transform.position) < 0.25f;
        }
    }
}