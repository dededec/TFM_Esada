/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class CameraProgression_Tutorial : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Movement _playerMovement;
        [SerializeField] private CinemachineDollyCart _cameraCart, _pointCart;
	  
	    #endregion
	
	    #region LifeCycle
	
        private void Update() 
        {
            var distance = _playerMovement.gameObject.transform.forward.normalized.z;
            Debug.Log("Distance: " + distance);
            if(_playerMovement.IsMoving && _playerMovement.transform.position.x < 3f && Mathf.Abs(distance) > 0.5f)
            {
                _cameraCart.m_Speed = distance/4f;
                _pointCart.m_Speed = distance/4f;
            }
            else
            {
                _cameraCart.m_Speed = 0;
                _pointCart.m_Speed = 0;
            }
        }
      
        #endregion
    }
}
