using UnityEngine;

namespace Raccons_House_Games
{
    public class EnemyWalkState : EnemyBaseState
    {
        public EnemyWalkState(EnemyControll enemyControll, Animator animator) : base (enemyControll, animator){}

        public override void OnEnter()
        {
            Debug.Log("Walk Enemy Start");
            _animator.CrossFade(WalkHash, crossFadeDuration);
        }

        public override void Update()
        {
            Debug.Log("Walking");
        }

        public override void OnExit()
        {
            Debug.Log("Walking Exit");
        }
    }
}
