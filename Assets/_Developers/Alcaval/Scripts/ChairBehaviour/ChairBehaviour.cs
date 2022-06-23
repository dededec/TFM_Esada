using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TFMEsada
{
    /// <summary>  
	/// Class that controls the chair behaviour and shows where it has to move
	/// </summary>
    public class ChairBehaviour : MonoBehaviour
    {
        #region Fields

        // Current state in the stateMachine
        public State _currentState;
        [SerializeField] private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private bool dead = false;
        public uint idWalkSound;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle
        

        private void Start() 
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        }

        void Update() 
        {
            RunStateMachine();
            
        }
      
        #endregion

        #region Private Methods
	    
        // Runs the stateMachine constantly so the agent, turn off to stop agent
        private void RunStateMachine()
        {
            State newState = _currentState?.RunCurrentState();

            if(newState != null){
                changeState(newState);
            }
        }

        // Assigns the new state of the stateMachine to the current one
        private void changeState(State newState)
        {
            _currentState = newState;
        }

        #endregion

        #region Public Methods

        public void playAwake()
        {
            idWalkSound = AkSoundEngine.PostEvent("silla_despierta", gameObject);
        }

        public void stopAwake()
        {
            AkSoundEngine.StopPlayingID(idWalkSound);
        }

        public void death()
        {
            stopAwake();
            Debug.Log("Se murio la silla");
            AkSoundEngine.PostEvent("silla_defeated", gameObject);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.GetComponent<ChairBehaviour>().enabled = false;
            _navMeshAgent.isStopped = true;
            _animator.SetBool("falling", true);
            //Destroy(gameObject);
        }

        public void fall()
        {
            // _animator.ResetTrigger("up");
            // _animator.SetTrigger("fall"); 
        }

        public void Attack()
        {
            AkSoundEngine.PostEvent("silla_atacando", gameObject);
            if(GetComponentInChildren<DamageCollider>() != null) GetComponentInChildren<DamageCollider>()._hitbox.enabled = true;
        }

         public void NotAttack()
        {
            GetComponentInChildren<DamageCollider>()._hitbox.enabled = false;
        }

        public void playFront()
        {
            AkSoundEngine.PostEvent("silla_pataDelantera", gameObject);
        }

        public void playBack()
        {
            AkSoundEngine.PostEvent("silla_pataTrasera", gameObject);
        }
            
        #endregion
    }
}