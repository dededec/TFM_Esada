using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Run state in stateMachine, used when agent is moving to player
	/// </summary>
    public class RunState : State
    {
        #region Fields



        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            return this;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}