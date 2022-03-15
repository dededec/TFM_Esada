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

        [Tooltip("Movement script to check if the player is moving and assign controls to this script.")]
        /// <summary>
        /// Movement script to check if the player is moving and assign controls to this script.
        /// </summary>
        [SerializeField] private Movement _movement;

        [Header("Balancing")]
        [Tooltip("Distance for the raycast that dictates if you can shoot an enemy.")]
        /// <summary>
        /// Distance for the raycast that dictates if you can shoot an enemy.
        /// </summary>
        [SerializeField] private float _aimDistance;

        [Tooltip("LayerMask which determines what objects can be shot.")]
        /// <summary>
        /// LayerMask which determines what objects can be shot.
        /// </summary>
        [SerializeField] private LayerMask _fireLayer;

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
                    _ammoSlider.value = Ammo;
                }
            }
        }

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            _shootNormal = _movement.Controls.Player.ShootNormal;

            if (_shootNormal == null)
            {
                Debug.LogError("Error: Fire Input Action is null");
            }

            _shootNormal.performed += shootNormal;
            _shootNormal.Enable();

            _shootPuddle = _movement.Controls.Player.ShootPuddle;
            _shootPuddle.performed += shootPuddle;
            _shootPuddle.Enable();
        }

        private void OnDisable()
        {
            _shootNormal.Disable();
            _shootPuddle.Disable();
        }

        private void Start() 
        {
            _ammoSlider.maxValue = Ammo;
            _ammoSlider.value = Ammo;
        }

        private void Update()
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * _aimDistance, Color.red);
        }

        #endregion

        #region Private Methods

        private void shootNormal(InputAction.CallbackContext context)
        {
            if (_movement.IsMoving)
            {
                Debug.Log("You can't shoot, you're moving.");
                return;
            }

            if(Ammo < _puddleCost)
            {
                Debug.Log("You can't shoot, not enough ammo.");
                return;
            }

            Debug.Log("Disparas normal");

            /*
            Creo que lo suyo es que sea en plan autoapuntado (Que no sea un proyectil que se cree),
            asi que trazamos un raycast palante y ver si está apuntando a un enemigo
            (tal vez sea mejor trazar un cono, pero esa parte se puede hacer después, es
            "un módulo aparte").
            Se traza un raycast, y si choca con algo se dispara (se rota hacia él? tal vez no hace falta).
            */
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _aimDistance, _fireLayer))
            {
                /* 
                Aquí faltarían más cosas, de animación, efectos visuales, sonidos y tal.
                De hecho tal vez se debería llamar a una función de una clase genérica enemigo,
                de la cual hereden todos los enemigos por su lado y puedan redefinir la función
                de muerte o algo por el estilo.
                */
                Debug.Log("Disparas a: " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
                Ammo -= _normalCost;
            }
        }

        private void shootPuddle(InputAction.CallbackContext context)
        {
            if (_movement.IsMoving)
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
            Ammo -= _puddleCost;
        }

        #endregion
    }
}
