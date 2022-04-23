using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Abstract class that specifies what a state of a state machine
    /// should have
	/// </summary>
    public abstract class State : MonoBehaviour
    {
	    #region Public Methods

        public abstract State RunCurrentState();

        #endregion
    }
}