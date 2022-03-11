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
      
        [Tooltip("Size of front and back collisions. Changing this value changes the size of the raycasts that trace collisions.")]
        [Range(0f, 1f)]
        /// <summary>  
	    /// Size of front and back collisions, traced via raycasts. The collisions have the shape of a triangle in front and behind the player.
	    /// </summary>
        [SerializeField] private float _collisionSize;

        [Tooltip("Forward movement speed. Measured in units/s. Must be greater or equal to 0f.")]
        [Min(0f)]
        /// <summary>  
	    /// Forward movement speed for the rigidbody, in units/s.
	    /// </summary>
        [SerializeField] private float _movementSpeed;

        [Tooltip("Input Action Asset mapping controls for this script.")]
        /// <summary>  
	    /// Input Action Asset mapping controls for this script.
	    /// </summary>
        private IAA_Player _playerControls;
        private InputAction _move;

        [Tooltip("Rigidbody used for movement and rotation.")]
        /// <summary>
        /// Rigidbody used for movement and rotation.
        /// </summary>
        [SerializeField] private Rigidbody _rigidbody;

        [Tooltip("Rotation speed. Measured in degrees/s. Must be greater or equal to 0f.")]
        [Min(0f)]
        /// <summary>  
	    /// Rotation speed for the rigidbody, in degrees/s.
	    /// </summary>
        [SerializeField] private float _rotationSpeed;
	  
	    #endregion

	 
	    #region LifeCycle

        private void Awake() 
        {
            _playerControls = new IAA_Player();
        }

        private void OnEnable() 
        {
            _move = _playerControls.Player.Move;
            _move.Enable();
        }

        private void OnDisable() 
        {
            _move.Disable();
        }
	  
        private void FixedUpdate() 
        {
            var movement = _move.ReadValue<Vector2>();
            var vertical = movement.y;
            var horizontal = movement.x;
            
            bool hasHitFront = Physics.Raycast(transform.position + transform.up, (transform.forward + transform.right), _collisionSize)
                        || Physics.Raycast(transform.position + transform.up, transform.forward, _collisionSize)
                        || Physics.Raycast(transform.position + transform.up, (transform.forward - transform.right), _collisionSize);

            bool hasHitBack = Physics.Raycast(transform.position + transform.up, (-transform.forward + transform.right), _collisionSize)
                        || Physics.Raycast(transform.position + transform.up, -transform.forward, _collisionSize)
                        || Physics.Raycast(transform.position + transform.up, (-transform.forward - transform.right), _collisionSize);

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
            
            // Tank controls: if the player isn't moving, they can rotate.
            if(vertical == 0f)
            {
                _rigidbody.rotation *= Quaternion.Euler(0, horizontal * _rotationSpeed, 0);
            }
        }
      
        #endregion
    }
}
