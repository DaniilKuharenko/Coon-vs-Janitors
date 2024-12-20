using UnityEngine;

namespace Raccons_House_Games
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerControll _playerControll;
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int WalkHash = Animator.StringToHash("Walking");
        protected static readonly int RunnHash = Animator.StringToHash("Running");
        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerControll playerControll, Animator animator)
        {
            _playerControll = playerControll;
            _animator = animator;
        }

        public virtual void OnEnter(){}
        public virtual void Update(){}
        public virtual void OnExit(){}

    }
}
