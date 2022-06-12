using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Idle state in stateMachine used when agent is doing nothing and hasn't seen the player yet
	/// </summary>
    public class FallState : State
    {
        #region Fields

        // States: Here you can have all the states that this state state could go to
        [SerializeField] private RunState _runState;
        [SerializeField] private Animator _animator;

        // Variable that lets us and the script know if the agent is inRange of the player
        public bool inRangePlayer{ set; get; }

        public bool up = false;

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(up)
            {
                up = false;
                return _runState;
            }
            else
            {
                return this;
            }
        }

        #endregion

        #region Public Methods

        public void PuddleColission()
        {
            _runState.fell = true;
            StartCoroutine(FallCoroutine(2f));
        }

        #endregion

        #region Coroutines

        IEnumerator FallCoroutine(float s)
        {
            yield return new WaitForSeconds(s);
            print("Se termino");
            up = true;
            _runState.fell = false;
        }

        #endregion
    }
}