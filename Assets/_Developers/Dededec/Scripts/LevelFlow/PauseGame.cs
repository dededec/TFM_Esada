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
        [SerializeField] private GameObject _uiGameplay;
        [SerializeField] private GameObject _uiDeath;
        [SerializeField] private ControlManager _controls;
        private InputAction _pauseUnpause;

        private void Start() 
        {
            assignControls();
        }

        private void OnEnable()
        {
            GameStateManager.instance.onGameStateChanged += onGameStateChanged;
            assignControls();
        }

        private void OnDisable()
        {
            GameStateManager.instance.onGameStateChanged -= onGameStateChanged;

            _pauseUnpause.started -= pauseUnpauseStarted;
            _pauseUnpause.Disable();
        }

        private bool assignControls()
        {

            if (_controls.Controls == null)
            {
                return false;
            }

            if (_pauseUnpause != null)
            {
                return true;
            }
            else
            {
                _pauseUnpause = _controls.Controls.UI.PauseUnpause;
                _pauseUnpause.started += pauseUnpauseStarted;
                _pauseUnpause.Enable();
                return true;
            }
        }


        private void pauseUnpauseStarted(InputAction.CallbackContext context)
        {
            _controls.CheckScheme(context.control.device.name);
            GameStateManager.instance.SetState(GameStateManager.instance.CurrentGameState == GameState.Gameplay || GameStateManager.instance.CurrentGameState == GameState.BeginLevel ? GameState.Paused : GameState.Gameplay);
        }

        private void onGameStateChanged(GameState newState)
        {
            _uiPause.SetActive(newState == GameState.Paused);
            _uiGameplay.SetActive(!(newState == GameState.Paused) && !_controls.IsDead);
            _uiDeath.SetActive(!(newState == GameState.Paused) && _controls.IsDead);

            _controls.StopPlayer(newState == GameState.Paused || newState == GameState.EndLevel);
        }
    }
}
