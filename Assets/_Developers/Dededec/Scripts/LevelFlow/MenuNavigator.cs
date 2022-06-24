/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class MenuNavigator : MonoBehaviour
    {
        [Serializable]
        public class ButtonTransition
        {
            [Serializable]
            public struct Transitions
            {
                public RectTransform up;
                public RectTransform down;
                public RectTransform right;
                public RectTransform left;
            }

            public Button button;

            public Transitions transitions;
            
        }

        #region Fields

        [SerializeField] private List<ButtonTransition> _buttons = new List<ButtonTransition>();
        [SerializeField] private RectTransform _selector;
        [SerializeField] private Sprite _smallSprite;
        [SerializeField] private Sprite _bigSprite;
        [SerializeField] private ControlManager _controlManager;  
        [SerializeField] private EventSystem eventSystem;
        private InputAction _uiSelect;
        private InputAction _uiNavigate;
        private bool _loadedControls = false;
        private int indexSelected;

        #endregion

        #region Life Cycle

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

            _uiNavigate.started -= changeSelection;
            _uiNavigate.Disable();

            _loadedControls = false;
        }

        #endregion

        #region Private Methods

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
            _controlManager.CheckScheme(context.control.device.name);
            AkSoundEngine.PostEvent("Enter", this.gameObject);
            ExecuteEvents.Execute(_buttons[indexSelected].button.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
        }

        private void changeSelection(InputAction.CallbackContext context)
        {
            _controlManager.CheckScheme(context.control.device.name);
            var values = context.ReadValue<Vector2>();
            AkSoundEngine.PostEvent("Click", this.gameObject);

            if(values.x > 0)
            {
                changeSelectorPosition(_buttons[indexSelected].transitions.right);
            }
            else if(values.x < 0)
            {
                changeSelectorPosition(_buttons[indexSelected].transitions.left);
            }

            if(values.y > 0)
            {
                changeSelectorPosition(_buttons[indexSelected].transitions.up);
            }
            else if(values.y < 0)
            {
                changeSelectorPosition(_buttons[indexSelected].transitions.down);
            }

            indexSelected = _buttons.FindIndex(b => b.button.gameObject.GetComponent<RectTransform>().position == _selector.position);
        }

        private void changeSelectorPosition(RectTransform rect)
        {
            if (rect != null)
            {
                _selector.position = rect.position;
                _selector.sizeDelta = rect.sizeDelta + Vector2.right * 20f;
                
                if(rect.gameObject.name.StartsWith("small"))
                {
                    _selector.GetComponent<Image>().sprite = _smallSprite;
                }
                else
                {
                    _selector.GetComponent<Image>().sprite = _bigSprite;
                }
            }
        }

        #endregion
    }
}
