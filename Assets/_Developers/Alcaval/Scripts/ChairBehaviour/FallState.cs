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
        private bool toRun = false;
        public bool fallStart = true;

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(up)
            {
                up = false;
                StartCoroutine(UpCoroutine(2f));
                return this;
            }
            else if(toRun)
            {
                toRun = false;
                _runState.justUp = true;
                return _runState;
            }
            else
            {
                StartCoroutine(FallCoroutine(4f));
                return this;
            }
        }

        #endregion

        #region Coroutines

        IEnumerator FallCoroutine(float s)
        {
            if(fallStart)
            {
                fallStart = false;
                yield return new WaitForSeconds(s);
                up = true;
                _runState.fell = false;
                StartCoroutine(UpCoroutine(2f));
            }
        }

        IEnumerator UpCoroutine(float s)
        {
            _animator.SetTrigger("up");
            _animator.ResetTrigger("fall");
            _runState.canFall = false;
            yield return new WaitForSeconds(0.3f);
            _animator.SetTrigger("awake");
            toRun = true;
            yield return new WaitForSeconds(2f);
            _runState.canFall = true;
            print("Se puede volver a caer");
            yield return null;
        }

        #endregion
    }
}