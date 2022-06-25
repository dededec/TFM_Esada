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
        [SerializeField] public Camera _renderCamera;


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
            if(_dialogPosition != null)
            {
                GetComponent<RectTransform>().anchoredPosition3D = _renderCamera.WorldToScreenPoint(_dialogPosition.position);

                if(_renderCamera.WorldToScreenPoint(_dialogPosition.position).x > _leftLimit.position.x && _renderCamera.WorldToScreenPoint(_dialogPosition.position).x < _rightLimit.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
                }else if(_renderCamera.WorldToScreenPoint(_dialogPosition.position).x < _leftLimit.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);         
                    transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
                }else{
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
      
        #endregion
    }
}