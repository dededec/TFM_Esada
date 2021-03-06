using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace TFMEsada
{
    /// <summary>  
	/// Manages the whole note
	/// </summary>
    public class NoteController : MonoBehaviour
    {
        #region Fields

        [Tooltip("ControlManager script to assign controls to this script.")]
        /// <summary>
        /// Movement script to assign controls to this script.
        /// </summary>
        [SerializeField] private ControlManager _controlManager;
        private InputAction _interact;


        [SerializeField] private Camera _renderCamera;
        private GameObject _speechBubble;
        private TMP_Text _text;
        private bool _inReadingRange = false;
        private GameObject _pressButtonProp;
        
        [Tooltip("Contents of the note no more than n characters")]
        [SerializeField] private string[] _contentOfNote;
        private int _currentDialog = 0;
        private TextWriter _tw;
	  
	    #endregion

        public bool Writing{ get; set;}
	 
	    #region LifeCycle
	  
        private void OnEnable() 
        {
            _speechBubble = GameObject.FindGameObjectWithTag("Bubble");
            _text = _speechBubble.transform.GetChild(1).GetComponent<TMP_Text>();
            _pressButtonProp = GameObject.FindGameObjectWithTag("ReadPropButton").transform.GetChild(0).gameObject;
            _tw = gameObject.GetComponent<TextWriter>();
        }

        private void OnDestroy() 
        {
            n1 = false;
            n2 = false;
        }

        private void Start() 
        {
            assignControls();
        }

        private void Update() 
        {
            if(_inReadingRange)
            {
                
                // _pressButtonProp.GetComponent<RectTransform>().anchoredPosition3D = _renderCamera.WorldToScreenPoint(transform.position);
                // if((Keyboard.current.spaceKey.wasPressedThisFrame) && Writing)
                // {
                //     _tw.FinishSentence();
                //     Writing = false;
                // }
                // else if((Keyboard.current.spaceKey.wasPressedThisFrame) && _currentDialog == _contentOfNote.Length && !Writing)
                // {
                //     _pressButtonProp.SetActive(true);
                //     _speechBubble.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                //     _speechBubble.transform.GetChild(0).gameObject.SetActive(false);
                //     _speechBubble.transform.GetChild(1).gameObject.SetActive(false);
                //     _currentDialog = 0;
                //     _inReadingRange = true;
                //     Writing = false;
                //     GameObject.FindGameObjectWithTag("Player").GetComponent<ControlManager>().TogglePlayerControls(true);
                // }
                // else if((Keyboard.current.spaceKey.wasPressedThisFrame) && _currentDialog < _contentOfNote.Length && !Writing)
                // {
                //     _pressButtonProp.SetActive(false);
                //     AkSoundEngine.PostEvent("PickUp_nota", this.gameObject);
                //     GameObject.FindGameObjectWithTag("Player").GetComponent<ControlManager>().TogglePlayerControls(false);
                //     //_pressButtonProp.SetActive(true);
                //     _speechBubble.transform.GetChild(0).gameObject.SetActive(true);
                //     _speechBubble.transform.GetChild(1).gameObject.SetActive(true);
                //     _tw.AddWriter(_speechBubble.GetComponentInChildren<TMP_Text>(), _contentOfNote[_currentDialog], 0.05f);
                //     Writing = true;
                //     _currentDialog++;
                // }

                RectTransform rt = _speechBubble.transform.GetChild(0).GetComponent<RectTransform>();
                Vector2 tSize = _text.GetRenderedValues(false);
                rt.GetComponent<RectTransform>().sizeDelta = new Vector2(rt.GetComponent<RectTransform>().sizeDelta.x, tSize.y + 30);

            }
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
                _interact.started += readNote;
                _interact.Enable();
                return true;
            }
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                _inReadingRange = true; 
                _pressButtonProp.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.tag == "Player")
            {
                _inReadingRange = false; 
                _pressButtonProp.SetActive(false);
            }
        }
	   
        #endregion

        #region Public Methods

        bool tamagochi = false;
        static bool n1 = false;
        static bool n2 = false;  

        public void readNote(InputAction.CallbackContext context)
        {
            if(!_inReadingRange || tamagochi) return;

            if(Writing)
            {
                _tw.FinishSentence();
                Writing = false;
            }
            else if(_currentDialog == _contentOfNote.Length && !Writing && !tamagochi)
            {
                _pressButtonProp.SetActive(true);
                _speechBubble.transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                _speechBubble.transform.GetChild(0).gameObject.SetActive(false);
                _speechBubble.transform.GetChild(1).gameObject.SetActive(false);
                _currentDialog = 0;
                _inReadingRange = true;
                Writing = false;
                GameObject.Find("Player").GetComponent<ControlManager>().TogglePlayerControls(true);
                Debug.Log("Nombre de la nota: " + gameObject.name);
                Debug.Log("n2: " + n2);
                
                if(gameObject.name == "TamagochiNote1")
                {
                    GameObject.FindGameObjectWithTag("Level2Controller").gameObject.GetComponent<Level2Controller>().UpdateTamagochiQuest();
                }

                if(gameObject.name == "TamagochiNoteFinal")
                {
                    gameObject.SetActive(false);
                    _pressButtonProp.SetActive(false);
                    tamagochi = true;
                    print("se entra");
                    GameObject.FindGameObjectWithTag("Level2Controller").gameObject.GetComponent<Level2Controller>().UpdateTamagochiQuest();
                }

                if(gameObject.name == "PhoneNote1" && !n1)
                {
                    Debug.Log("Nota 1 leida");
                    n1 = true;
                    GameObject.FindGameObjectWithTag("Level2Controller").gameObject.GetComponent<Level2Controller>().UpdatePhone();
                }

                if(gameObject.name == "PhoneNote2" && !n2)
                {
                    Debug.Log("Nota 2 leida");
                    n2 = true;
                    GameObject.FindGameObjectWithTag("Level2Controller").gameObject.GetComponent<Level2Controller>().UpdatePhone();
                }
                
                if(gameObject.name == "PhoneNote4")
                {
                    GameObject.FindGameObjectWithTag("Level2Controller").gameObject.GetComponent<Level2Controller>().UpdatePhone();
                    //CameraShaker.Instance.ShakeOnce(4f,4f,.1f,.1f);
                }
            }
            else if(_currentDialog < _contentOfNote.Length && !Writing)
            {
                _pressButtonProp.SetActive(false);
                AkSoundEngine.PostEvent("PickUp_nota", this.gameObject);
                GameObject.Find("Player").GetComponent<ControlManager>().TogglePlayerControls(false);
                //_pressButtonProp.SetActive(true);
                _speechBubble.transform.GetChild(0).gameObject.SetActive(true);
                _speechBubble.transform.GetChild(1).gameObject.SetActive(true);
                _tw.AddWriter(_speechBubble.GetComponentInChildren<TMP_Text>(), _contentOfNote[_currentDialog], 0.05f);
                Writing = true;
                _currentDialog++;
            }
        }

        #endregion
    }
}