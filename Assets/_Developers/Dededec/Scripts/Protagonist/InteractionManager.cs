/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TFMEsada
{
    /// <summary>  
	/// Handle key obtaining and door opening. 
	/// </summary>
    public class InteractionManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Level2Controller level2Controller;

        private float _keyCount = 0;

        [Tooltip("ControlManager script to assign controls to this script.")]
        /// <summary>
        /// Movement script to assign controls to this script.
        /// </summary>
        [SerializeField] private ControlManager _controlManager;
        private InputAction _interact;

        [Tooltip("NoteController script to manage note reading.")]
        /// <summary>
        /// NoteController script to manage note reading.
        /// </summary>
        [SerializeField] private NoteController _noteController;

        [Tooltip("Range in which the player can pick a key or open a locked door.")]
        /// <summary>
        /// Range in which the player can pick a key or open a locked door.
        /// </summary>
        [SerializeField] private float _range;

        [Tooltip("Layer in which to look for keys.")]
        /// <summary>
        /// Layer in which to look for keys.
        /// </summary>
        [SerializeField] private LayerMask _keyLayer;
        
        [Tooltip("Layer in which to look for doors.")]
        /// <summary>
        /// Layer in which to look for doors.
        /// </summary>
        [SerializeField] private LayerMask _doorLayer;

        [Tooltip("Layer in which to look for the final door.")]
        /// <summary>
        /// Layer in which to look for the final door.
        /// </summary>
        [SerializeField] private LayerMask _endingLayer;

        [Tooltip("Layer in which to look for collectables.")]
        /// <summary>
        /// Layer in which to look for collectables.
        /// </summary>
        [SerializeField] private LayerMask _collectableLayer;

        [Tooltip("UI to show when the level is completed.")]
        /// <summary>
        /// UI to show when the level is completed.
        /// </summary>
        [SerializeField] private GameObject _victoryUI;

        private bool hasCollectable = false;

	    #endregion

        #region Properties

        private bool[] hasTamagotchi
        {
            get
            {
                return SaveDataController.HasTamagotchi;
            }

            set
            {
                SaveDataController.HasTamagotchi = value;
            }
        }

        private int currentLevelIndex
        {
            get
            {
                return SaveDataController.CurrentLevelIndex;
            }

            set
            {
                SaveDataController.CurrentLevelIndex = value;
            }
        }
        
        #endregion

        #region LifeCycle

        private void OnEnable() 
        {
            assignControls();
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

        private void OnDisable() 
        {
            StopControls();
        }

        private void Update() 
        {
            Debug.DrawRay(transform.position + transform.up, transform.forward * _range, Color.cyan);
        }

        #endregion

        #region Public Methods

        public void StopControls()
        {
            _interact.started -= interact;
            _interact.Disable();
        }

        #endregion

        #region Private Methods
        
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

        private void interact(InputAction.CallbackContext context)
        {   
            RaycastHit hit;
            if(RayCast(out hit, _range, _keyLayer))
            {
                // Obtener llave.
                Debug.Log("Apañaste llave.");
                AkSoundEngine.PostEvent("PickUp_llave", this.gameObject);
                _keyCount++;
                Destroy(hit.collider.gameObject);
                return;
            }
            else if(RayCast(out hit, _range, _collectableLayer))
            {   
                Debug.Log("Pillas coleccionable");
                hasCollectable = true;
                Destroy(hit.collider.gameObject);
                return;
            }
            else if(RayCast(out hit, _range, _doorLayer))
            {
                var sign = Mathf.Sign(Vector3.SignedAngle(hit.collider.gameObject.transform.right, transform.forward, Vector3.up));
                StartCoroutine(crOpenDoor(hit.collider.gameObject, sign));
                if(hit.collider.gameObject.name == "SecondFloorDoor") level2Controller.TeleportPlayer(1); 
                if(hit.collider.gameObject.name == "FirstFloorDoor") level2Controller.TeleportPlayer(0);
            }
            else if(RayCast(out hit, _range, _endingLayer))
            {
                // Abrir puerta si hay llave.
                if(_keyCount > 0)
                {
                    Debug.Log("Abres puerta.");
                    _keyCount--;
                    victory();
                }
                return;
            }

            Debug.Log("No se pilla nada");
        }

        private IEnumerator crOpenDoor(GameObject door, float sign)
        {
            Animator anim = door.GetComponent<Animator>();
            
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("IdleDoor"))
            {
                Debug.Log("La puerta no está en idle.");
                yield break;
            }

            anim.SetFloat("Sign", sign);
            yield return new WaitForSeconds(5f);
            anim.SetFloat("Sign", 0f);
            anim.SetTrigger("Close");
        }

        private void victory()
        {
            _victoryUI.SetActive(true);
            _controlManager.TogglePlayerControls(false);

            /*
            Esto se podría hacer escalable si tuviesemos una convención
            para los nombres de escena, por ejemplo "Mundo_NúmeroDeNivel" o algo así
            */
            int levelIndex = -1;
            switch(SceneManager.GetActiveScene().name)
            {
                case "Tutorial":
                levelIndex = 0;
                break;
                case "Level1":
                levelIndex = 1;
                break;
                case "Level2":
                levelIndex = 2;
                break;
                default:
                Debug.LogError("Error: Nombre de nivel no encontrado: " + SceneManager.GetActiveScene().name);
                return;
            }
            
            if(hasCollectable)
            {
                // Se harán cosas en el _victoryUI
                hasTamagotchi[levelIndex] = true;
            }

            // Si estábamos jugando el último nivel, se desbloquea el siguiente
            if(levelIndex == currentLevelIndex)
            {
                currentLevelIndex++;
            }
        }

        private bool RayCast(out RaycastHit hit, float range, LayerMask layerMask)
        {
            return Physics.Raycast(transform.position + transform.up, transform.forward, out hit, range, layerMask, QueryTriggerInteraction.Collide);
        }
	   
        #endregion
    }
}
