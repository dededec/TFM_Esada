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
        [SerializeField] private ChairBehaviour chairBehaviour;
        // Variable that lets us and the script know if the agent is inRange of the player
        public bool inRangePlayer{ set; get; }
        public bool switchMode = false;
        private GameObject _player;

        #endregion

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            chairBehaviour = gameObject.transform.parent.parent.gameObject.GetComponent<ChairBehaviour>();
            _player = GameObject.Find("Player");
        }

        #region Public Methods
        public override State RunCurrentState()
        {
            if(_player.GetComponent<ControlManager>().PlayerDead)
            {
                return this;
            }

            if(inRangePlayer)
            {
                chairBehaviour.playAwake();
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
            //AkSoundEngine.PostEvent("silla_despierta", _animator.gameObject);
        }

        #endregion
    }
}