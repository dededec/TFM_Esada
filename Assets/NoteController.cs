using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class NoteController : MonoBehaviour
    {
        #region Fields
      
        private GameObject _speechBubble;
	  
	    #endregion
	 
	    #region LifeCycle
	  
        private void OnEnable() {
            _speechBubble = GameObject.FindGameObjectWithTag("Bubble");
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods
	   
        #endregion
    }
}