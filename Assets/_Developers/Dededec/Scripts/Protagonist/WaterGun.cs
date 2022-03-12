/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class WaterGun : MonoBehaviour
    {
        #region Fields

        [Tooltip("Movement script to check if the player is moving.")]
        /// <summary>
        /// Movement script to check if the player is moving.
        /// </summary>
        [SerializeField] private Movement _movement;

        private InputAction _fire;
	  
	    #endregion
	 
	    #region LifeCycle
	
        // private void OnEnable() 
        // {
        //     _fire = _movement.Controls.Player.Fire;
        //     _fire.performed += fire;
        //     _fire.Enable();
        // }

        // private void OnDisable() 
        // {
        //     _fire.Disable();
        // }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods

        private void fire(InputAction.CallbackContext context)
        {
            if(!_movement.IsMoving)
            {
                Debug.Log("Disparaste");
            }
        }
	   
        #endregion
    }
}
