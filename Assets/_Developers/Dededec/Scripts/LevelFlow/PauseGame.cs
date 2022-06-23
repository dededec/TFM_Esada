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
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPause;
        [SerializeField] private ControlManager _controls;
        private InputAction _pauseUnpause;

        private void Start() 
        {
            GameStateManager.instance.onGameStateChanged += onGameStateChanged;

            _pauseUnpause = _controls.Controls.UI.PauseUnpause;
            _pauseUnpause.started += pauseUnpauseStarted;
            _pauseUnpause.Enable();

        }

        private void OnDestroy() 
        {
            GameStateManager.instance.onGameStateChanged -= onGameStateChanged;    

            _pauseUnpause.started -= pauseUnpauseStarted;
            _pauseUnpause.Disable();
        }

        
        private void pauseUnpauseStarted(InputAction.CallbackContext context)
        {
            GameStateManager.instance.SetState(GameStateManager.instance.CurrentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay);
        }

        private void onGameStateChanged(GameState newState)
        {
            _uiPause.SetActive(newState == GameState.Paused);
            _controls.StopPlayer(newState == GameState.Paused);
        }
    }
}
