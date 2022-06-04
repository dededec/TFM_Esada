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
using UnityEngine.EventSystems;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class LevelSelectionTamagotchi : MonoBehaviour
    {
        #region Fields
      
        // [Tooltip("Public variables set in the Inspector, should have a Tooltip")]
        /// <summary>  
	    /// They should also have a summary
	    /// </summary>
        // public static string Ejemplo;

        [Header("UI")]
        [SerializeField] private Button RightButton;
        [SerializeField] private Button LeftButton;
        [SerializeField] private EventSystem eventSystem;

        [Header("Level settings")]
        [SerializeField] private GameObject _levelHolder;
        [SerializeField] private int _selectedIndex;
        private List<GameObject> _levels = new List<GameObject>();

        [SerializeField] private GameFlowController _gameFlowController;


        [Header("Controls")]
        [SerializeField] private InputAction _scrollControls;
        [SerializeField] private InputAction _pickControls;
        [SerializeField] private AnimationCurve _scrollCurve;
        private Coroutine _scrollCoroutine;
	  
	    #endregion

	    #region LifeCycle
	  
        private void OnEnable() 
        {
            for(int i=0; i<_levelHolder.transform.childCount; ++i)
            {
                _levels.Add(_levelHolder.transform.GetChild(i).gameObject);
            }

            _selectedIndex = 0;          

            _scrollControls.started += processInput;
            _scrollControls.Enable();

            _pickControls.started += pickLevel; 
            _pickControls.Enable();
        }

        private void OnDisable() 
        {
            _scrollControls.Disable();
            _pickControls.Disable();
        }
      
        #endregion


        #region Private Methods

        private void processInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            if(value > 0)
            {
                ExecuteEvents.Execute(RightButton.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
            else
            {
                ExecuteEvents.Execute(LeftButton.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
            // StartScrollCoroutine(value);
        }

        public void StartScrollCoroutine(float value)
        {
            if(_scrollCoroutine == null)
            {
                _scrollCoroutine = StartCoroutine(crScroll(value));
            }
        }

        private void pickLevel(InputAction.CallbackContext context)
        {
            if(context.ReadValue<float>() == 1)
            {
                if(_levels[_selectedIndex].GetComponent<Tamagotchi>().isLocked)
                {
                    _gameFlowController.LoadScene(_levels[_selectedIndex].name);
                }
                else
                {
                    Debug.Log("Nivel no desbloqueado.");
                }
            }
            else
            {
                Debug.Log("Se sale al menú");
            }
        }

        private IEnumerator crScroll(float value)
        {
            if((_selectedIndex == 0 && value < 0) || (_selectedIndex == _levels.Count - 1 && value > 0)) 
            {
                yield break;
            }

            float duration = 0.75f;
            float rotation = _levelHolder.transform.rotation.eulerAngles.y;
            float direction = 40 * value;
            for(float i=0; i < duration; i += Time.deltaTime)
            {
                float aux = Mathf.Lerp(0f, 1f, i/duration);
                aux = _scrollCurve.Evaluate(aux);
                _levelHolder.transform.rotation = Quaternion.Euler(Vector3.up * (rotation + direction * aux));
                yield return null;
            }

            _levelHolder.transform.rotation = Quaternion.Euler(Vector3.up * (rotation + direction));
            _selectedIndex += (int) value;
            _scrollCoroutine = null;
        }
	   
        #endregion
    }
}
