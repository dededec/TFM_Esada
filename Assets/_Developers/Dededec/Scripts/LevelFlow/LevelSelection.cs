/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class LevelSelection : MonoBehaviour
    {
        #region Fields

        [SerializeField] private InputAction _scrollControls;
        [SerializeField] private InputAction _pickControls;
        [SerializeField] private ScrollRect _levelScroll;
        [SerializeField] private List<Object> _scenes;
        private List<string> _sceneNames;
        private int _sceneToLoad;
        [SerializeField] private GameFlowController _gameFlowController;

        private Coroutine scrollCoroutine = null;

	    #endregion
	 
	    #region LifeCycle
	  
        // Start, OnAwake, Update, etc
        private void OnEnable() 
        {
            _scrollControls.performed += processInput;
            _scrollControls.Enable();
            _sceneToLoad = 0;

            _pickControls.started += pickLevel; 
            _pickControls.Enable();
        }

        private void OnDisable() 
        {
            _scrollControls.Disable();
            _pickControls.Disable();
        }

        private void Start() 
        {
            _sceneNames = new List<string>();
            foreach(var obj in _scenes)
            {
                _sceneNames.Add(obj.name);
            }
        }
      
        #endregion

        #region Private Methods

        private void processInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            if(scrollCoroutine == null)
            {
                // scrollCoroutine = StartCoroutine(crScroll(value));
            }
        }

        private void pickLevel(InputAction.CallbackContext context)
        {
            Debug.Log("STARTED");
            if(context.ReadValue<float>() == 1)
            {
                _gameFlowController.LoadScene(_sceneNames[_sceneToLoad]);
            }
            else
            {
                Debug.Log("Se sale del nivel");
            }
        }

        // private IEnumerator crScroll(float direction)
        // {
        //     // Si son tres niveles, cada uno está en 0 - 0.5 - 1
        //     // Si fuesen cuatro, sería 0 - 0.33 - 0.66 - 1
        //     _sceneToLoad += (int) direction;
        //     if(_sceneToLoad < 0) _sceneToLoad = 0;
        //     else if(_sceneToLoad > 2) _sceneToLoad = 2;
        //     var goal = _sceneToLoad * 0.5f; 
        //     var start = _levelScroll.horizontalNormalizedPosition;
        //     var duracion = 0.1f;
        //     for(float i=0; i<duracion; i+=Time.deltaTime)
        //     {
        //         _levelScroll.horizontalNormalizedPosition = Mathf.Lerp(start, goal, i/duracion);
        //         yield return null;
        //     }   

        //     _levelScroll.horizontalNormalizedPosition = goal;
        //     scrollCoroutine = null;
        // }
	   
        #endregion
    }
}
