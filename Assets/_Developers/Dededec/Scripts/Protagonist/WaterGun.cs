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
	/// Brief summary of what the class does
	/// </summary>
    public class WaterGun : MonoBehaviour
    {
        #region Fields

        [Tooltip("Movement script to check if the player is moving.")]
        /// <summary>
        /// Movement script to check if the player is moving.
        /// </summary>
        [SerializeField] private Movement _movement;

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


        private InputAction _fire;
	  
	    #endregion
	 
	    #region LifeCycle
	
        private void OnEnable() 
        {
            _fire = _movement.Controls.Player.Fire;
            
            if(_fire == null)
            {
                Debug.LogError("Error: Fire Input Action is null");
            }

            _fire.performed += fire;
            _fire.Enable();
        }

        private void OnDisable() 
        {
            _fire.Disable();
        }

        private void Update() 
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * _aimDistance, Color.red);
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods

        private void fire(InputAction.CallbackContext context)
        {
            if(!_movement.IsMoving)
            {
                Debug.Log("Disparaste");
            }

            /*
            Creo que lo suyo es que sea en plan autoapuntado (Que no sea un proyectil que se cree),
            asi que trazamos un raycast palante y ver si está apuntando a un enemigo
            (tal vez sea mejor trazar un cono, pero esa parte se puede hacer después, es
            "un módulo aparte").
            Se traza un raycast, y si choca con algo se dispara (se rota hacia él? tal vez no hace falta).
            */
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, _aimDistance, _fireLayer))
            {
                /* 
                Aquí faltarían más cosas, de animación, efectos visuales, sonidos y tal.
                De hecho tal vez se debería llamar a una función de una clase genérica enemigo,
                de la cual hereden todos los enemigos por su lado y puedan redefinir la función
                de muerte o algo por el estilo.
                */
                Debug.Log("Disparas a: " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject); 
            }
        }
	   
        #endregion
    }
}
