/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class RestartManager : MonoBehaviour
    {
        [SerializeField] private GameFlowController _gfc;
        [SerializeField] private ControlManager _controlManager;
        private InputAction _restart;
        [SerializeField] private TMP_Text _restartText;

        private void OnEnable() 
        {
            _restart = _controlManager.Controls.UI.Restart;
            _restart.started += RestartLevel;
            _restart.Enable();
            
            changeRestartText(_controlManager.CurrentScheme);
            _controlManager.onControlSchemeChanged.AddListener(changeRestartText);
        }

        private void OnDisable() 
        {
            _restart.started -= RestartLevel;
            _restart.Disable();

            _controlManager.onControlSchemeChanged.RemoveListener(changeRestartText);
        }

        private void changeRestartText(ControlManager.ControlScheme scheme)
        {
            if(scheme == ControlManager.ControlScheme.MOUSEKEYBOARD)
            {
                _restartText.text = "PULSA R PARA REINICIAR";
            }
            else if(scheme == ControlManager.ControlScheme.GAMEPAD)
            {
                _restartText.text = "PULSA X PARA REINICIAR";
            }
        }

        private void RestartLevel(InputAction.CallbackContext context)
        {
            _gfc.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
