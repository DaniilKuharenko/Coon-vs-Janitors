using UnityEngine;

namespace Raccons_House_Games
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerControll playerControll, Animator animator) : base (playerControll, animator){}

        public override void OnEnter()
        {
            Debug.Log("Idle Start");
            _animator.CrossFade(IdleHash, crossFadeDuration);
        }

        public override void Update()
        {
            Debug.Log("Ideling");
        }

        public override void OnExit()
        {
            Debug.Log("Idle Exit");
        }
    }
}
