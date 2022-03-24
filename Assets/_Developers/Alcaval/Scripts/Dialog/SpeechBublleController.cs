using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class SpeechBublleController : MonoBehaviour
    {
        #region Fields
      
        private Transform _dialogPosition;
        private Transform _leftLimit;
        private Transform _rightLimit;
	  
	    #endregion
	 
	    #region LifeCycle

        private void Start() {
            _dialogPosition = GameObject.FindGameObjectWithTag("DialogPosition").transform;
            _leftLimit = GameObject.FindGameObjectWithTag("LeftLimit").transform;
            _rightLimit = GameObject.FindGameObjectWithTag("RightLimit").transform;
        }
	  
        private void Update() {
            transform.position = Camera.main.WorldToScreenPoint(_dialogPosition.position);

            if(Camera.main.WorldToScreenPoint(_dialogPosition.position).x > _leftLimit.position.x && Camera.main.WorldToScreenPoint(_dialogPosition.position).x < _rightLimit.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if(Camera.main.WorldToScreenPoint(_dialogPosition.position).x < _leftLimit.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);         
                transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
            }else{
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
      
        #endregion
    }
}