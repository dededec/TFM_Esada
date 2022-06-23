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
        
        [SerializeField] private GameObject _uiPause;
        private InputAction _pauseUnpause;

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
            GameStateManager.instance.onGameStateChanged += onGameStateChanged;
        }

        private void Start() 
        {
            _pauseUnpause = Controls.UI.PauseUnpause;
            _pauseUnpause.started += pauseUnpauseStarted;
            _pauseUnpause.Enable();

        }

        private void OnDestroy() 
        {
            StopPlayer(true);
            _pauseUnpause.started -= pauseUnpauseStarted;
            _pauseUnpause.Disable();
            GameStateManager.instance.onGameStateChanged -= onGameStateChanged;    
        }
      
        #endregion

        #region Public Methods

        public void PlayerDeath()
        {
            //...
            GetComponentInChildren<Animator>().SetTrigger("IsDead");
            AkSoundEngine.PostEvent("Player_Defeated", this.gameObject);
            StopPlayer(true);
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

        // Paro Interaction para que no pueda interactuar con objetos
        // mientras el jugador esté muerto.
        public void StopPlayer(bool value)
        {
            if(value)
            {
                Controls.Player.Disable();
                Controls.Camera.Disable();
                Controls.Interaction.Disable();
            }
            else
            {
                Controls.Player.Enable();
                Controls.Camera.Enable();
                Controls.Interaction.Enable();
            }
        }

        private void pauseUnpauseStarted(InputAction.CallbackContext context)
        {
            GameStateManager.instance.SetState(GameStateManager.instance.CurrentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay);
        }

        private void onGameStateChanged(GameState newState)
        {
            _uiPause.SetActive(newState == GameState.Paused);
            StopPlayer(newState == GameState.Paused);
        }

        #endregion
    }
}
