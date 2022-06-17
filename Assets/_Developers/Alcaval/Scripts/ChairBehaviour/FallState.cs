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
        public bool fallStart = true;
        public int upFlag = 0;

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            print("alo");
            if(up && upFlag > 1)
            {
                up = false;
                upFlag = 0;
                return _runState;
            }
            else
            {
                StartCoroutine(FallCoroutine(2f));
                _animator.SetTrigger("fall");
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
            if(fallStart)
            {
                fallStart = false;
                yield return new WaitForSeconds(s);
                print("Se termino");
                up = true;
                _runState.fell = false;
            }
        }

        IEnumerator UpCoroutine()
        {
            upFlag++;
            _animator.SetTrigger("fall");
            yield return new WaitForSeconds(1f);
            upFlag++;
        }

        #endregion
    }
}