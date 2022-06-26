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
	/// Brief summary of what the class does
	/// </summary>
    public class MainMenuFunctions : MonoBehaviour
    {
        [SerializeField] private GameFlowController _gfc;
        [SerializeField] private GameObject _uiAjustes;
        [SerializeField] private GameObject _selectorTitle;
        [SerializeField] private MenuNavigator _navigatorTitle;

        public void StartGame()
        {
            // Trigger de texto
            _gfc.LoadScene("Level_Selection");
        }

        public void ToogleSettings(bool value)
        {
            Debug.Log("VAFEFE: " + value);
            _navigatorTitle.enabled = !value;
            _selectorTitle.SetActive(!value);
            _uiAjustes.SetActive(true);
        }
    }
}
