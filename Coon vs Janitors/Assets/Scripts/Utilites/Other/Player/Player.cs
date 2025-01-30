using UnityEngine;

namespace Raccons_House_Games
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerControll _playerControll;
        [SerializeField] private Animator _animator;
        private StateMachine _stateMachine;
        private WalkState _walkState;
        private IdleState _idleState;

        public void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();
            _walkState = new WalkState(_playerControll, _animator);
            _idleState = new IdleState(_playerControll, _animator);
            _stateMachine.AddTransition(_idleState, _walkState, new Predicate(() => _playerControll.CheckSpeed >= 0.5));
            _stateMachine.AddTransition(_walkState, _idleState, new Predicate(() => _playerControll.CheckSpeed <= 0.5));

            _stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _stateMachine?.Update();
            Debug.LogError($"Speeed {_playerControll.CheckSpeed}");
        }
    }
}
