/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class MainMenuFunctions : MonoBehaviour
    {
        [SerializeField] private GameFlowController _gfc;
        [SerializeField] private GameObject _uiAjustes;
        [SerializeField] private GameObject _uiMenu;
        [SerializeField] private GameObject _selectorTitle;
        [SerializeField] private MenuNavigator _navigatorTitle;
        [SerializeField] private MenuNavigator _navigatorAjustes;

        public void StartGame()
        {
            // Trigger de texto
            AkSoundEngine.StopAll();
            _gfc.LoadScene("LevelSelection");
            
        }

        public void ToogleSettingsOn()
        {
            _navigatorTitle.enabled = false;
            _selectorTitle.SetActive(false);
            _uiMenu.SetActive(false);
            foreach(var button in _uiMenu.GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }

            _uiAjustes.SetActive(true);
            _navigatorAjustes.enabled = true;
            StartCoroutine(ToggleButtons(_uiAjustes, true));
        }

        public void ToogleSettingsOff()
        {
            _navigatorAjustes.enabled = false;
            _uiAjustes.SetActive(false);
            foreach(var button in _uiAjustes.GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }

            _uiMenu.SetActive(true);
            _selectorTitle.SetActive(true);
            _navigatorTitle.enabled = true;
           StartCoroutine(ToggleButtons(_uiMenu, true));
        }

        /*
            Se espera un frame para activar los botones ya que, sin saber
            muy bien por qué, por ejemplo, se activaría el botón de activar 
            el menú de ajustes y a la vez el botón que estuviese seleccionado
            en dicho menú. Para desactivar no hay ese problema.
        */
        private IEnumerator ToggleButtons(GameObject ui, bool value)
        {
            yield return null;
            foreach(var button in ui.GetComponentsInChildren<Button>())
            {
                button.interactable = value;
            }
        }
    }
}
