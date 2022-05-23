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

        public void death()
        {
            Debug.Log("Se murio la silla");
            _navMeshAgent.isStopped = true;
            _animator.SetTrigger("fall");
            this.GetComponent<ChairBehaviour>().enabled = false;
            //Destroy(gameObject);
        }
            
        #endregion
    }
}