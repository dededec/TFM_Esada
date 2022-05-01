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

namespace TFMEsada
{
    /// <summary>  
	/// Functions to use at the end of a level
	/// </summary>
    public class LevelCompletedFunctions : MonoBehaviour
    {
        [Tooltip("Scenes to use in order (Level Selection, Tutorial, First Level, ...)")]
        /// <summary>
        /// Scenes to use in order (Level Selection, Tutorial, First Level, ...)
        /// </summary>
        [SerializeField] private List<Object> _sceneList;

        private List<string> _scenes;

        [SerializeField] private GameFlowController _gameFlowController;

        private void Start() 
        {
            _scenes = new List<string>();
            foreach(var obj in _sceneList)
            {
                _scenes.Add(obj.name);
            }
        }

	    #region Public Methods
        
        public void LoadNextLevel()
        {
            var index = _scenes.FindIndex(0, _scenes.Count, scene => scene == SceneManager.GetActiveScene().name);
            if(_gameFlowController != null)
            {
                _gameFlowController.LoadScene(_scenes[index+1]);
            }
            else
            {
                SceneManager.LoadScene(_scenes[index+1]);
            }
        }

        public void GoToSelect()
        {
            if(_gameFlowController != null)
            {
                _gameFlowController.LoadScene(_scenes[0]);
            }
            else
            {
                SceneManager.LoadScene(_scenes[0]);
            }
        }

        #endregion

    }
}
