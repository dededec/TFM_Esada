/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Determines how the bullet shot via WaterGun.cs behaves when instatiated.
	/// </summary>
    public class BulletBehaviour : MonoBehaviour
    {	 
        #region Fields

        [Tooltip("Bullet speed set via Rigidbody.velocity .")]
        /// <summary>
        /// Bullet speed set via Rigidbody.velocity .
        /// </summary>
        [SerializeField] private float _speed = 5f;

        #endregion

        #region Fields

        public float Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
                GetComponent<Rigidbody>().velocity = transform.forward * _speed;
            }
        }

        #endregion

	    #region LifeCycle
	  
        private void Start() 
        {
            GetComponent<Rigidbody>().velocity = transform.forward * _speed;    
            Destroy(this.gameObject, 10f);
        }
      
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Enemy")
            {
                if(other.GetComponent<ChairBehaviour>() != null) { other.GetComponent<ChairBehaviour>().death(); }
            }
            
            if(other.tag != "Player" && other.tag != "Note" && other.tag != "FX" && other.tag != "AI")
            {
                Debug.Log("Se destruye con: " + other.name + " --- " + other.tag);
                Destroy(this.gameObject);
            }
        }
	   
        #endregion
    }
}
