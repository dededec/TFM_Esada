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
	/// Handle key obtaining and door opening. 
	/// </summary>
    public class KeyManager : MonoBehaviour
    {
        #region Fields

        private float _keyCount = 0;

        [Tooltip("Range in which the player can pick a key or open a locked door.")]
        /// <summary>
        /// Range in which the player can pick a key or open a locked door.
        /// </summary>
        [SerializeField] private float _range;

        [Tooltip("ControlManager script to assign controls to this script.")]
        /// <summary>
        /// Movement script to assign controls to this script.
        /// </summary>
        [SerializeField] private ControlManager _controlManager;
        private InputAction _interact;

        [Tooltip("Layer in which to look for keys.")]
        /// <summary>
        /// Layer in which to look for keys.
        /// </summary>
        [SerializeField] private LayerMask _keyLayer;
        
        [Tooltip("Layer in which to look for doors.")]
        /// <summary>
        /// Layer in which to look for doors.
        /// </summary>
        [SerializeField] private LayerMask _doorLayer;

        [Tooltip("Layer in which to look for collectables.")]
        /// <summary>
        /// Layer in which to look for collectables.
        /// </summary>
        [SerializeField] private LayerMask _collectableLayer;

        [Tooltip("UI to show when the level is completed.")]
        /// <summary>
        /// UI to show when the level is completed.
        /// </summary>
        [SerializeField] private GameObject _victoryUI;

        private bool hasCollectable = false;

	    #endregion

        #region LifeCycle

        private void OnEnable() 
        {
            assignControls();
        }

        /*
        Note: I use Start() besides OnEnable() because it is NOT guaranteed that
        this script's OnEnable() function will execute BEFORE ControlManager's Awake() function.
        For reference: https://forum.unity.com/threads/onenable-before-awake.361429/
        */
        private void Start() 
        {
            assignControls();
        }

        private void OnDisable() 
        {
            StopControls();
        }

        private void Update() 
        {
            Debug.DrawRay(transform.position + transform.up, transform.forward * _range, Color.cyan);
        }

        #endregion

        #region Public Methods

        public void StopControls()
        {
            _interact.started -= interact;
            _interact.Disable();
        }

        #endregion

        #region Private Methods

        private void interact(InputAction.CallbackContext context)
        {   
            RaycastHit hit;
            if(Physics.Raycast(transform.position + transform.up, transform.forward, out hit, _range, _keyLayer, QueryTriggerInteraction.Collide))
            {
                // Obtener llave.
                Debug.Log("Apañaste llave.");
                _keyCount++;
                Destroy(hit.collider.gameObject);
                return;
            }
            else if(Physics.Raycast(transform.position + transform.up, transform.forward, out hit, _range, _doorLayer, QueryTriggerInteraction.Collide))
            {
                // Abrir puerta si hay llave.
                if(_keyCount > 0)
                {
                    Debug.Log("Abres puerta.");
                    _keyCount--;
                    victory();
                }
                return;
            }
            else if(Physics.Raycast(transform.position + transform.up, transform.forward, out hit, _range, _collectableLayer, QueryTriggerInteraction.Collide))
            {
                Debug.Log("Pillas coleccionable");
                hasCollectable = true;
                Destroy(hit.collider.gameObject);
                return;
            }

            Debug.Log("No se pilla nada");
        }

        private bool assignControls()
        {
            if (_controlManager.Controls == null)
            {
                return false;
            }
            else
            {
                _interact = _controlManager.Controls.Interaction.Interact;
                _interact.started += interact;
                _interact.Enable();
                return true;
            }
        }

        private void victory()
        {
            _victoryUI.SetActive(true);
            if(hasCollectable)
            {
                // Se harán cosas
            }
        }
	   
        #endregion
    }
}
