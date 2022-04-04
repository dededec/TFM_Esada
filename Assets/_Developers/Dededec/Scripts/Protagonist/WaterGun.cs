/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TFMEsada
{
    /// <summary>  
	/// Water gun management, both mechanics and UI
	/// </summary>
    public class WaterGun : MonoBehaviour
    {
        #region Fields

        #region General settings

        [Tooltip("ControlManager script to assign controls to this script.")]
        /// <summary>
        /// Movement script to assign controls to this script.
        /// </summary>
        [SerializeField] private ControlManager _controlManager;

        [Tooltip("Movement script to check if the player is moving.")]
        /// <summary>
        /// Movement script to check if the player is moving.
        /// </summary>
        [SerializeField] private Movement _movement;

        [Tooltip("Projectile to instantiate when doing a normal shot.")]
        /// <summary>
        /// Projectile to instantiate when doing a normal shot.
        /// </summary>
        [SerializeField] private GameObject _bullet;

        [Tooltip("Projectile to instantiate when doing a puddle shot.")]
        /// <summary>
        /// Projectile to instantiate when doing a puddle shot.
        /// </summary>
        [SerializeField] private GameObject _puddle;

        [Tooltip("Transform from which to instantiate the projectile.")]
        /// <summary>
        /// Transform from which to instantiate the projectile.
        /// </summary>
        [SerializeField] private Transform _shootPosition;

        [Tooltip("Transform from which to instantiate the puddle.")]
        /// <summary>
        /// Transform from which to instantiate the puddle.
        /// </summary>
        [SerializeField] private Transform _puddlePosition;

        #endregion

        #region Balancing

        [Header("Balancing")]
        [Tooltip("Distance for the raycast that dictates if you can shoot an enemy.")]
        /// <summary>
        /// Distance for the raycast that dictates if you can shoot an enemy.
        /// </summary>
        [SerializeField] private float _aimDistance;

        #endregion

        #region Ammo Options

        [Header("Ammo options")]
        [Tooltip("Amount of shots the player has for this level.")]
        /// <summary>
        /// Amount of shots the player has for this level.
        /// </summary>
        [SerializeField] private int _ammo;

        [Tooltip("Slider showing player ammo.")]
        /// <summary>
        /// Slider showing player ammo.
        /// </summary>
        [SerializeField] private Slider _ammoSlider;

        [Tooltip("Ammo cost of a normal shot.")]
        /// <summary>
        /// Ammo cost of a normal shot.
        /// </summary>
        [SerializeField] private int _normalCost;

        [Tooltip("Ammo cost of a puddle shot.")]
        /// <summary>
        /// Ammo cost of a puddle shot.
        /// </summary>
        [SerializeField] private int _puddleCost;

        #endregion

        private InputAction _shootNormal;
        private InputAction _shootPuddle;

        #endregion

        #region Properties

        public int Ammo
        {
            get
            { 
                return _ammo; 
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be greater than 0");
                }
                else
                {
                    _ammo = value;
                    
                    if(_ammoSlider != null)
                    {
                        _ammoSlider.value = Ammo;
                    }
                }
            }
        }

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            assignControls();
        }

        private void OnDisable()
        {
            _shootNormal.performed -= shootNormal;
            _shootPuddle.performed -= shootPuddle;

            _shootNormal.Disable();
            _shootPuddle.Disable();
        }

        /*
        Note: I use Start() besides OnEnable() because it is NOT guaranteed that
        this script's OnEnable() function will execute BEFORE ControlManager's Awake() function.
        For reference: https://forum.unity.com/threads/onenable-before-awake.361429/
        */
        private void Start() 
        {
            assignControls();


            if(_ammoSlider != null)
            {
                _ammoSlider.maxValue = Ammo;
                _ammoSlider.value = Ammo;
            }
        }

        private void Update()
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * _aimDistance, Color.red);
        }

        #endregion

        #region Private Methods

        private void shootNormal(InputAction.CallbackContext context)
        {
            if (_movement.IsMoving || _movement.IsRotating)
            {
                Debug.Log("You can't shoot, you're moving.");
                return;
            }

            if(Ammo < _normalCost)
            {
                Debug.Log("You can't shoot, not enough ammo.");
                return;
            }

            Debug.Log("Disparas normal");
            Instantiate(_bullet, _shootPosition.position, transform.rotation);
            Ammo -= _normalCost;
            
        }

        private void shootPuddle(InputAction.CallbackContext context)
        {
            if (_movement.IsMoving || _movement.IsRotating)
            {
                Debug.Log("You can't shoot, you're moving.");
                return;
            }

            if(Ammo < _puddleCost)
            {
                Debug.Log("You can't shoot, not enough ammo.");
                return;
            }

            Debug.Log("Disparas charco");
            
            Instantiate(_puddle, _puddlePosition.position, transform.rotation);
            Ammo -= _puddleCost;
        }

        private bool assignControls()
        {
            if (_controlManager.Controls == null)
            {
                return false;
            }
            else
            {
                _shootNormal = _controlManager.Controls.Player.ShootNormal;

                _shootNormal.performed += shootNormal;
                _shootNormal.Enable();

                _shootPuddle = _controlManager.Controls.Player.ShootPuddle;
                _shootPuddle.performed += shootPuddle;
                _shootPuddle.Enable();
                return true;
            }
        }

        #endregion
    }
}
