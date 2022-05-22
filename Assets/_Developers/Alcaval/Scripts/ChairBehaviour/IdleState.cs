using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Idle state in stateMachine used when agent is doing nothing and hasn't seen the player yet
	/// </summary>
    public class IdleState : State
    {
        #region Fields

        // States: Here you can have all the states that this state state could go to
        [SerializeField] private RunState _runState;
        [SerializeField] private Animator _animator;

        // Variable that lets us and the script know if the agent is inRange of the player
        public bool inRangePlayer{ set; get; }

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(inRangePlayer)
            {
                return _runState;
            }
            else
            {
                return this;
            }
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                StartCoroutine(PursuingCoroutine(2f));
            }
        }

        #endregion

        #region Coroutines

        IEnumerator PursuingCoroutine(float s)
        {
            yield return new WaitForSeconds(s);
            inRangePlayer = true;
            _animator.SetTrigger("awake");
        }

        #endregion
    }
}