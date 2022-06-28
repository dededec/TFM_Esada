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
    public class ExitGame : MonoBehaviour
    {
        #region Fields
      
        [SerializeField] private ControlManager _controlManager;
        private InputAction _cancel;
        private bool _loadedControls;

        #endregion

        #region LifeCycle

        private void Start() 
        {
            if(!_loadedControls)
            {
                assignControls();
            }
        }

        private void OnEnable() 
        {
            if(!_loadedControls)
            {
                assignControls();
            }
        }

        private void OnDisable() 
        {
            _cancel.started -= Exit;
            _cancel.Disable();

            _loadedControls = false;
        }

        #endregion

        #region Private Methods

        private void assignControls()
        {
            if (_controlManager.Controls == null)
            {
                return;
            }
            else
            {
                _cancel = _controlManager.Controls.UI.Cancel;
                _cancel.started += Exit;
                _cancel.Enable();
                _loadedControls = true;
            }
        }

        private void Exit(InputAction.CallbackContext context)
        {
            Debug.Log("SE QUITA");
            Application.Quit();
        }
	   
        #endregion
    }
}
