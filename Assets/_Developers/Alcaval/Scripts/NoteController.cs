using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class NoteController : MonoBehaviour
    {
        #region Fields
      
        private GameObject _speechBubble;
        private TMP_Text _text;

        private bool _isReading = false;
        private bool _control;

        [SerializeField] private Image _pressButtonProp;


        [Tooltip("Contents of the note no more than n characters")]
        [SerializeField] private string[] _contentOfNote;
        private int _currentDialog = 0;
	  
	    #endregion
	 
	    #region LifeCycle
	  
        private void OnEnable() {
            _speechBubble = GameObject.FindGameObjectWithTag("Bubble");
        }

        private void Update() {
            if(_isReading)
            {
                _pressButtonProp.gameObject.SetActive(true);
                _pressButtonProp.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                
                if((Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame) && _currentDialog == _contentOfNote.Length)
                {
                    _pressButtonProp.gameObject.SetActive(true);
                    _speechBubble.transform.GetChild(0).gameObject.SetActive(false);
                    _currentDialog = 0;
                    _isReading = true;
                }
                else if((Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame) && _currentDialog < _contentOfNote.Length)
                {
                    _pressButtonProp.gameObject.SetActive(true);
                    _speechBubble.transform.GetChild(0).gameObject.SetActive(true);
                    _speechBubble.GetComponentInChildren<TMP_Text>().SetText(_contentOfNote[_currentDialog]);
                    _currentDialog++;
                }

            }
        }
        
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player")
            {
                _isReading = true; 
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag == "Player")
            {
                _isReading = false; 
                _pressButtonProp.gameObject.SetActive(false);
            }
        }
	   
        #endregion
    }
}