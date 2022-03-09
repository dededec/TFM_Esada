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

        // States: Here you can have all the states that this state
        //         state could go to
        [SerializeField] private RunState _runState;

        // Variable that lets us and the script know if the agent is inRange of the player
        public bool inRangePlayer;
        

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(inRangePlayer){
                return _runState;
            }else{
                return this;
            }
        }

        #endregion
    }
}