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

        private Rigidbody _rb;
        private Vector3 _pausedVelocity;
        private Vector3 _pausedAngularVelocity;

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
                _rb.velocity = transform.forward * _speed;
            }
        }

        #endregion

        #region LifeCycle

        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();
            GameStateManager.instance.onGameStateChanged += onGameStateChanged;
        }

        private void Start()
        {
            _rb.velocity = transform.forward * _speed;
            Destroy(this.gameObject, 10f);
        }

        private void OnDestroy()
        {
            GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision with: " + other.gameObject.name);
            if (other.tag == "Enemy")
            {
                AkSoundEngine.PostEvent("choque_disparo_enemigo", this.gameObject);
                if (other.GetComponent<ChairBehaviour>() != null) { other.GetComponent<ChairBehaviour>().death(); }
                if (other.GetComponent<BookColisionDetection>() != null) { other.GetComponent<BookColisionDetection>().death(); }
            }

            if (other.tag != "Player" && other.tag != "Note" && other.tag != "FX" && other.tag != "AI")
            {
                // Debug.Log("Se destruye con: " + other.name + " --- " + other.tag);
                AkSoundEngine.PostEvent("choque_disparo_pared", this.gameObject);
                Destroy(this.gameObject);
            }
        }

        private void onGameStateChanged(GameState newGameState)
        {
            switch (GameStateManager.instance.CurrentGameState)
            {
                case GameState.Gameplay:
                    ResumeRigidbody();
                    break;
                case GameState.Paused:
                case GameState.EndLevel:
                    PauseRigidbody();
                    break;
                    
                default:
                    break;
            }
        }

        private void PauseRigidbody()
        {
            _pausedVelocity = _rb.velocity;
            _pausedAngularVelocity = _rb.angularVelocity;
            _rb.isKinematic = true;
        }

        private void ResumeRigidbody()
        {
            _rb.isKinematic = false;
            _rb.velocity = _pausedVelocity;
            _rb.angularVelocity = _pausedAngularVelocity;
        }

        #endregion
    }
}
