/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TFMEsada
{
    /// <summary>  
	/// Functions to use at the end of a level
	/// </summary>
    public class LevelCompletedManager : MonoBehaviour
    {
        // No es static porque si es const es static en C#
        private const string LevelSelectionScene = "Test_LevelSelection";


        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private ControlManager _controlManager;  
        private InputAction _uiSelect;
        private InputAction _uiNavigate;
        private bool _loadedControls = false;

        [SerializeField] private string _nextLevel;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private RectTransform[] _buttonTransform = new RectTransform[2];

        private void Start() 
        {
            if(!_loadedControls)
            {
                assignControls();
            }
        }

        private void OnEnable() 
        {
            if(!_loadedControls)
            {
                assignControls();
            }
        }

        private void OnDisable() 
        {
            _uiSelect.started -= buttonInteract;
            _uiSelect.Disable();

            _uiSelect.started -= changeSelection;
            _uiNavigate.Disable();

        }

        private void assignControls()
        {
            if (_controlManager.Controls == null)
            {
                return;
            }
            else
            {
                _uiSelect = _controlManager.Controls.UI.Select;
                _uiSelect.started += buttonInteract;
                _uiSelect.Enable();

                _uiNavigate = _controlManager.Controls.UI.Navigate;
                _uiNavigate.started += changeSelection;
                _uiNavigate.Enable();

                _loadedControls = true;
            }
        }

        private void buttonInteract(InputAction.CallbackContext context)
        {
            if(_selector.position == _buttonTransform[0].position)
            {
                ExecuteEvents.Execute(_buttonTransform[0].gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
            else
            {
                ExecuteEvents.Execute(_buttonTransform[1].gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
        }

        private void changeSelection(InputAction.CallbackContext context)
        {
            var values = context.ReadValue<Vector2>();

            if(values.x > 0 && _selector.position == _buttonTransform[0].position)
            {
                _selector.position = _buttonTransform[1].position;
            }
            else if(values.x < 0 && _selector.position == _buttonTransform[1].position)
            {
                _selector.position = _buttonTransform[0].position;
            }
        }

	    #region Public Methods
        
        public void LoadNextLevel()
        {
            if(_gameFlowController != null)
            {
                _gameFlowController.LoadScene(_nextLevel);
            }
            else
            {
                Debug.LogError("Error: GameFlowController no encontrado.");
            }
        }

        public void GoToSelect()
        {
            if(_gameFlowController != null)
            {
                _gameFlowController.LoadScene(LevelSelectionScene);
            }
            else
            {
                Debug.LogError("Error: GameFlowController no encontrado.");
            }
        }

        #endregion

    }
}
