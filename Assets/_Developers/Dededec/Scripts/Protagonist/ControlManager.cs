/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TFMEsada
{
    /// <summary>  
	/// Creates controls Class for other scripts to use.
	/// </summary>
    public class ControlManager : MonoBehaviour
    {

        public enum ControlScheme
        {
            MOUSEKEYBOARD,
            GAMEPAD,
        }

        #region Fields

        public class ControlSchemeEvent : UnityEvent<ControlScheme> { }
        public ControlSchemeEvent onControlSchemeChanged = new ControlSchemeEvent();
        public bool PlayerDead = false;

        [SerializeField] private GameObject _uiDeath;
        [SerializeField] private GameObject _uiGameplay;
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

        public ControlScheme CurrentScheme
        {
            get;
            private set;
        }

        public bool IsDead
        {
            get;
            private set;
        }

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _controls = new IAA_Player();
            CurrentScheme = ControlScheme.GAMEPAD;
        }

        private void OnDestroy()
        {
            StopPlayer(true);
        }

        #endregion

        #region Public Methods

        public void PlayerDeath()
        {
            PlayerDead = true;
            GetComponentInChildren<Animator>().SetTrigger("IsDead");
            AkSoundEngine.PostEvent("Player_Defeated", this.gameObject);
            DeadPlayerControls();
            _uiDeath.SetActive(true);
            _uiGameplay.SetActive(false);
            IsDead = true;
        }

        public void TogglePlayerControls(bool value)
        {
            if (value)
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
            if (IsDead) return;
            if (value)
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

        public void DeadPlayerControls()
        {
            if(IsDead) return;
            Controls.Player.Disable();
            Controls.Interaction.Disable();
        }

        public void CheckScheme(string device)
        {
            var scheme = CurrentScheme;
            var dev = device.ToLower();
            if (dev.Contains("gamepad"))
            {
                CurrentScheme = ControlScheme.GAMEPAD;
            }
            else if (dev.Contains("mouse") || dev.Contains("keyboard"))
            {
                CurrentScheme = ControlScheme.MOUSEKEYBOARD;
            }

            if (scheme != CurrentScheme)
            {
                onControlSchemeChanged?.Invoke(CurrentScheme);
            }
        }

        #endregion
    }
}
