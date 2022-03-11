using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Attack state in stateMachine used when agent close enough to player and able to attack
	/// </summary>
    public class AttackState : State
    {
        #region Fields

        // States: Here you can have all the states that this state
        //         state could go to
        [SerializeField] private IdleState _idleState;
        [SerializeField] private RunState _runState;
        [SerializeField] private Animator animator;

        // Flag that controls whether you have finished an attack or not
        public bool finishedAttacking;

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(finishedAttacking){
                finishedAttacking = false;
                print("aqui?");
                print(_runState);
                return _runState;
            }
            StartCoroutine(AttackCoroutine(0f));
            finishedAttacking = true;
            return this;
        }

        #endregion

        #region Private Methods
        
        #endregion

        #region Coroutines

        IEnumerator AttackCoroutine(float s)
        {
            yield return new WaitForSeconds(s);
            animator.SetTrigger("attack");
            finishedAttacking = true;
        }

        #endregion
    }
}