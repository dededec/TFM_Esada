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
        
        private string _keyMask = "Key";
        private string _doorMask = "Door";
        private float _keyCount = 0;

	    #endregion

        #region LifeCycle

        private void OnEnable() 
        {
            assignControls();
        }

        /*
        Note: I use Start() instead of OnEnable() because it is NOT guaranteed that
        this script's OnEnable() function will execute BEFORE ControlManager's Awake() function.
        For reference: https://forum.unity.com/threads/onenable-before-awake.361429/
        */
        private void Start() 
        {
            assignControls();
        }

        private void OnDisable() 
        {
            _interact.started -= interact;
            _interact.Disable();
        }

        #endregion

        #region Private Methods

        private void interact(InputAction.CallbackContext context)
        {   
            RaycastHit hit;
            if(Physics.Raycast(transform.position + transform.up, (transform.forward + transform.right), out hit, _range, LayerMask.NameToLayer(_keyMask)))
            {
                // Obtener llave.
                _keyCount++;
                Destroy(hit.collider.gameObject);
            }
            else if(Physics.Raycast(transform.position + transform.up, (transform.forward + transform.right), out hit, _range, LayerMask.NameToLayer(_doorMask)))
            {
                // Abrir puerta si hay llave.
                if(_keyCount > 0)
                {
                    _keyCount--;
                }
            }
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
	   
        #endregion
    }
}
