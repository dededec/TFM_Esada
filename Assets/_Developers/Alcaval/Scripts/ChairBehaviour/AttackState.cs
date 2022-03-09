using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Attack state in stateMachine used when agent close enough to player and able to attack
	/// </summary>
    public class AttackState : State
    {
        #region Fields

        // States: Here you can have all the states that this state
        //         state could go to
        [SerializeField] private IdleState _idleState;


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