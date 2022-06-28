using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class Controller : MonoBehaviour
    {

        [SerializeField] private InputAction _controls;
        private bool reading = false;

        private void OnEnable() {
            _controls.Enable();
        }

        private void OnDisable() {
            _controls.Disable();
        }

        private void Update() 
        {
            if(!reading)
            {
                var movement = _controls.ReadValue<Vector2>();
                Vector3 Movement = new Vector3 (movement.x, 0, movement.y);
                transform.position += Movement * 10f * Time.deltaTime;
            }
        }

        public void setReading(bool flag){
            reading = flag;
        }
    }
}