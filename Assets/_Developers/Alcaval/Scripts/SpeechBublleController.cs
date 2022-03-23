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

            if(Camera.main.WorldToScreenPoint(_player.position).x > 2 || Camera.main.WorldToScreenPoint(_player.position).x < 5)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods
	   
        #endregion
    }
}