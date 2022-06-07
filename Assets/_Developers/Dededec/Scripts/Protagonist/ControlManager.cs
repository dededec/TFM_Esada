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
	/// Creates controls Class for other scripts to use.
	/// </summary>
    public class ControlManager : MonoBehaviour
    {
        #region Fields

        private IAA_Player _controls;

	    #endregion
	  
	    #region Properties
	  
        public IAA_Player Controls
        {
            get
            {
                return _controls;
            }
        }
            
	    #endregion
	 
	    #region LifeCycle
	  
        private void Awake() 
        {
            _controls = new IAA_Player();
        }
      
        #endregion

        #region Public Methods

        public void PlayerDeath()
        {
            //...
            GetComponent<Animator>().SetTrigger("IsDead");
            Debug.LogWarning("Muerte del jugador no implementada");
        }

        public void TogglePlayerControls(bool value)
        {
            if(value)
            {
                Controls.Player.Enable();
                Controls.Camera.Enable();
            }
            else
            {
                Controls.Player.Disable();
                Controls.Camera.Disable();
            }
        }

        #endregion
    }
}
