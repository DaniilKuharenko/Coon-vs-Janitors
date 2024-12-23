using UnityEngine;

namespace Raccons_House_Games
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyControll enemyControll, Animator animator) : base (enemyControll, animator){}

        public override void OnEnter()
        {
            Debug.Log("Idle Start");
            animator.CrossFade(EnemyIdleHash, crossFadeDuration);
        }

        public override void Update()
        {
        }

        public override void OnExit()
        {
            
        }

    }
}
