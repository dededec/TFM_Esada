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
    public class CameraProgression : MonoBehaviour
    {
        #region Fields
      
        // [Tooltip("Public variables set in the Inspector, should have a Tooltip")]
        /// <summary>  
	    /// They should also have a summary
	    /// </summary>
        // public static string Ejemplo;
	  
	    // private float _ejemplo;

        public Cinemachine.CinemachineDollyCart CameraCart, PivotCart;

        public InputAction Controls;

        private float posActual = 0, maxPos = 10;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle
	  
        // Start, OnAwake, Update, etc
        private void OnEnable() 
        {
            Controls.performed += moveTracks;
            Controls.Enable();
        }

        private void OnDisable() 
        {
            Controls.Disable();
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods

        private void moveTracks(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            posActual += input.x;
            if(posActual < 0) posActual = 0;
            else if(posActual > maxPos) posActual = maxPos;
            var pos = Mathf.Lerp(0, 1, posActual/maxPos);
            CameraCart.m_Position = pos;
            PivotCart.m_Position = pos;

        }
	   
        #endregion
    }
}
