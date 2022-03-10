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
        private IdleState _idleState;
        private RunState _runState;

        public bool inAttackRange;

        #endregion

        #region LifeCycle

        private void Start() {
            _runState = gameObject.transform.parent.GetComponentInChildren<RunState>();
            _idleState = gameObject.transform.parent.GetComponentInChildren<IdleState>();
        }
      
        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(inAttackRange){
                StartCoroutine(AttackCoroutine(2f));
                return _idleState;
            }else{
                return _runState;
            }
        }

        #endregion

        #region Private Methods
        private void OnTriggerExit(Collider other) {
            inAttackRange = false;
        }

        private void OnTriggerStay(Collider other) {
            inAttackRange = true;
        }

        private void OnTriggerEnter(Collider other) {
            inAttackRange = true;
        }
        
        #endregion

        #region Coroutines

        IEnumerator AttackCoroutine(float s){
            yield return new WaitForSeconds(s);
        }

        #endregion
    }
}