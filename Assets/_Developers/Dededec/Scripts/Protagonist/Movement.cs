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
	/// Tank controls of the player
	/// </summary>
    public class Movement : MonoBehaviour
    {
        #region Fields

        [Header("Collisions")]
        [Tooltip("Size of front and back collisions. Changing this value changes the size of the raycasts that trace collisions.")]
        [Range(0f, 1f)]
        /// <summary>  
	    /// Size of front and back collisions, traced via raycasts. The collisions have the shape of a triangle in front and behind the player.
	    /// </summary>
        [SerializeField] private float _collisionSize;

        [Tooltip("Collision Mask to determine which objects influence movement. Player layer shouldn't be ticked.")]
        /// <summary>  
	    /// Collision Mask to determine which objects influence movement. Player Layer shouldn't be ticked as it could lead to
        /// no movement allowed.
	    /// </summary>
        [SerializeField] private LayerMask _collisionMask; 

        [Header("Movement parameters")]
        [Tooltip("Rigidbody used for movement and rotation.")]
        /// <summary>
        /// Rigidbody used for movement and rotation.
        /// </summary>
        [SerializeField] private Rigidbody _rigidbody;

        [Tooltip("Forward movement speed. Measured in units/s. Must be greater or equal to 0f.")]
        [Min(0f)]
        /// <summary>  
	    /// Forward movement speed for the rigidbody, in units/s.
	    /// </summary>
        [SerializeField] private float _movementSpeed;

        [Tooltip("Rotation speed. Measured in degrees/s. Must be greater or equal to 0f.")]
        [Min(0f)]
        /// <summary>  
	    /// Rotation speed for the rigidbody, in degrees/s.
	    /// </summary>
        [SerializeField] private float _rotationSpeed;

        [Header("Controls")]
        private IAA_Player _controls;
        private InputAction _move;

	    #endregion

        #region Properties

        public bool IsMoving
        {
            get
            {
                if(_move.enabled)
                {
                    return (_move.ReadValue<Vector2>().x != 0f) || (_move.ReadValue<Vector2>().y != 0f);
                }
                else
                {
                    return false;
                }
            }
        }

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
        }

        private void OnEnable() 
        {
            _move = Controls.Player.Move;
            _move.Enable();
        }

        private void OnDisable() 
        {
            _move.Disable();
        }
	  
        private void FixedUpdate() 
        {
            var input = _move.ReadValue<Vector2>();
            movement(input);
            rotation(input);
        }
      
        #endregion

        #region Private Methods

        private void movement(Vector2 input)
        {
            var vertical = input.y;
            var horizontal = input.x;
            
            bool hasHitFront = Physics.Raycast(transform.position + transform.up, (transform.forward + transform.right), _collisionSize, _collisionMask)
                        || Physics.Raycast(transform.position + transform.up, transform.forward, _collisionSize, _collisionMask)
                        || Physics.Raycast(transform.position + transform.up, (transform.forward - transform.right), _collisionSize, _collisionMask);

            bool hasHitBack = Physics.Raycast(transform.position + transform.up, (-transform.forward + transform.right), _collisionSize, _collisionMask)
                        || Physics.Raycast(transform.position + transform.up, -transform.forward, _collisionSize, _collisionMask)
                        || Physics.Raycast(transform.position + transform.up, (-transform.forward - transform.right), _collisionSize, _collisionMask);

            Debug.DrawRay(transform.position + transform.up, transform.forward * _collisionSize, Color.green);
            Debug.DrawRay(transform.position + transform.up, (transform.forward + transform.right).normalized * _collisionSize , Color.green);
            Debug.DrawRay(transform.position + transform.up, (transform.forward - transform.right).normalized * _collisionSize , Color.green);

            Debug.DrawRay(transform.position + transform.up, -transform.forward * _collisionSize, Color.green);
            Debug.DrawRay(transform.position + transform.up, (-transform.forward + transform.right).normalized * _collisionSize , Color.green);
            Debug.DrawRay(transform.position + transform.up, (-transform.forward - transform.right).normalized * _collisionSize , Color.green);

            if(!hasHitFront && vertical > 0f)
            {
                // MovePosition() because physics are not neccessary (i.e. using _rigidbody.velocity).
                _rigidbody.MovePosition(_rigidbody.transform.position + _movementSpeed * Time.deltaTime * vertical * _rigidbody.transform.forward); 
            }

            if(!hasHitBack && vertical < 0f)
            {
                // MovePosition() because physics are not neccessary (i.e. using _rigidbody.velocity).
                _rigidbody.MovePosition(_rigidbody.transform.position + _movementSpeed * Time.deltaTime * vertical * _rigidbody.transform.forward); 
            }
        }

        private void rotation(Vector2 input)
        {
            // Tank controls: if the player isn't moving, they can rotate.
            if(input.y == 0f)
            {
                _rigidbody.rotation *= Quaternion.Euler(0, input.x * _rotationSpeed, 0);
            }
        }

        #endregion
    }
}
