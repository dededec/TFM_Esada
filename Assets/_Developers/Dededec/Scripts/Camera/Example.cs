/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class Example : MonoBehaviour
    {
        #region Fields
      
        // [Tooltip("Public variables set in the Inspector, should have a Tooltip")]
        /// <summary>  
	    /// They should also have a summary
	    /// </summary>
        // public static string Ejemplo;
	  
	    // private float _ejemplo;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle
	  
        // Start, OnAwake, Update, etc
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods
	   
        #endregion
    }
}
