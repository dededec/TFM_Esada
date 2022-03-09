using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class StateMachine : MonoBehaviour
    {
        #region Fields

        // Current state that the agent is currently on
        State currentState;
	  
	    #endregion
	 
	    #region LifeCycle
	  
        void Update() {
            RunStateMachine();
        }
      
        #endregion

        #region Private Methods
	   
       // Runs the stateMachine constantly so the agent, turn off to stop agent
        private void RunStateMachine(){
            State newState = currentState?.RunCurrentState();

            if(newState != null){
                changeState(newState);
            }
        }

        // Assigns the new state of the stateMachine to the current one
        private void changeState(State newState){
            currentState = newState;
        }

        #endregion
    }
}