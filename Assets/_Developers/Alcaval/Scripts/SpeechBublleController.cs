using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class SpeechBublleController : MonoBehaviour
    {
        #region Fields
      
        [SerializeField] private Transform _player;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle
	  
        private void Update() {
            transform.position = Camera.main.WorldToScreenPoint(_player.position);
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods
	   
        #endregion
    }
}