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
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Controls;
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
        [SerializeField] private WaterSlider _ammoSlider;

        [Tooltip("Ammo cost of a normal shot.")]
        /// <summary>
        /// Ammo cost of a normal shot.
        /// </summary>
        [SerializeField] private int _normalCost = 1;

        [Tooltip("Ammo cost of a puddle shot.")]
        /// <summary>
        /// Ammo cost of a puddle shot.
        /// </summary>
        [SerializeField] private int _puddleCost = 2;

        #endregion

        [SerializeField] private Animator _animator;

        private InputAction _shoot;

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
                    if (_ammoSlider != null)
                    {
                        // value = _ammo + x => x = value - _ammo
                        // Debug.Log("WATER: " + (0.5f * (float) (value - _ammo)));
                        _ammoSlider.Water += (0.5f * (float)(value - _ammo));
                    }

                    _ammo = value;
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
            StopControls();
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

        private void Update()
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * _aimDistance, Color.red);
        }

        #endregion

        #region Public Methods

        public void StopControls()
        {
            _shoot.performed -= Shoot;
            _shoot.Disable();
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            _controlManager.CheckScheme(context.control.device.name);
            if (context.interaction is HoldInteraction)
                shootPuddle();
            else
                shootNormal();
        }

        public void OnShootingAnimation()
        {
            AkSoundEngine.PostEvent("Disparar_pistola_agua", this.gameObject);
            StartCoroutine(crShoot());
        }

        public void OnShootingPuddleAnimation()
        {
            AkSoundEngine.PostEvent("Disparar_charco", this.gameObject);
            StartCoroutine(crCreatePuddle());
        }

        #endregion

        #region Private Methods

        private void shootNormal()
        {
            if (_movement.IsMoving || _movement.IsRotating)
            {
                Debug.Log("You can't shoot, you're moving.");
                return;
            }

            if (Ammo < _normalCost)
            {
                Debug.Log("You can't shoot, not enough ammo.");
                return;
            }

            _animator.SetTrigger("IsShootingNormal");
            Ammo -= _normalCost;
        }

        private void shootPuddle()
        {
            if (_movement.IsMoving || _movement.IsRotating)
            {
                Debug.Log("You can't shoot, you're moving.");
                return;
            }

            if (Ammo < _puddleCost)
            {
                Debug.Log("You can't shoot, not enough ammo.");
                return;
            }

            _animator.SetTrigger("IsShootingPuddle");
            Ammo -= _puddleCost;
        }

        private IEnumerator crCreatePuddle()
        {
            var puddle = Instantiate(_puddle, _puddlePosition.position, transform.rotation).transform;
            var duracion = 0.8f;
            float ogScale = puddle.localScale.x;
            for (float i = 0; i < duracion; i += Time.deltaTime)
            {
                float newScale = Mathf.Lerp(ogScale, 1f, i / duracion);
                puddle.localScale = Vector3.one * newScale;
                yield return null;
            }

            puddle.localScale = Vector3.one;
        }

        private IEnumerator crShoot()
        {
            Instantiate(_bullet, _shootPosition.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }

        private bool assignControls()
        {
            if (_controlManager.Controls == null)
            {
                return false;
            }

            if (_shoot != null)
            {
                return true;
            }
            else
            {
                _shoot = _controlManager.Controls.Player.Shoot;
                _shoot.performed += Shoot;
                _shoot.Enable();

                return true;
            }
        }

        #endregion
    }
}
