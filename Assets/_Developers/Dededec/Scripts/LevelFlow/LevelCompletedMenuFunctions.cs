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
    public class LevelCompletedMenuFunctions : MonoBehaviour
    {
        // No es static porque si es const es static en C#
        private const string LevelSelectionScene = "LevelSelection";
        [SerializeField] private GameFlowController _gameFlowController;
        [SerializeField] private string _nextLevel;

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
