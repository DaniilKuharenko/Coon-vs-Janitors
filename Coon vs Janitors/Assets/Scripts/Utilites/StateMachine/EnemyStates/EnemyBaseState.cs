using UnityEngine;

namespace Raccons_House_Games
{
    public abstract class EnemyBaseState : IState
    {
        protected readonly EnemyControll _enemyControll;
        protected readonly Animator _animator;

        protected static readonly int EnemyIdleHash = Animator.StringToHash("EnemyIdle");
        protected static readonly int WalkHash = Animator.StringToHash("Walking");
        protected static readonly int RunnHash = Animator.StringToHash("Running");
        protected const float crossFadeDuration = 0.1f;

        protected EnemyBaseState(EnemyControll enemyControll, Animator animator)
        {
            _enemyControll = enemyControll;
            _animator = animator;
        }

        public virtual void OnEnter(){}
        public virtual void Update(){}
        public virtual void OnExit(){}

    }
}
