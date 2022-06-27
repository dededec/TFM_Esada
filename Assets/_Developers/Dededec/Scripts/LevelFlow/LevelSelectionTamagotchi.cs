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
        private int _selectedIndex;
        private List<Tamagotchi> _levels = new List<Tamagotchi>();

        [SerializeField] private GameFlowController _gameFlowController;

        [Header("Controls")]
        [SerializeField] private ControlManager _controlManager;
        private InputAction _scrollControls;
        private InputAction _selectControls;
        private InputAction _cancelControls;
        private bool _loadedControls = false;

        [SerializeField] private AnimationCurve _scrollCurve;
        private Coroutine _scrollCoroutine;

        #endregion

        #region Properties

        private bool[] hasTamagotchi
        {
            get
            {
                return SaveDataController.HasTamagotchi;
            }

            set
            {
                SaveDataController.HasTamagotchi = value;
            }
        }

        private int currentLevelIndex
        {
            get
            {
                return SaveDataController.CurrentLevelIndex;
            }

            set
            {
                SaveDataController.CurrentLevelIndex = value;
            }
        }
	  
	    #endregion

	    #region LifeCycle

        private void Start() 
        {
            if(!_loadedControls)
            {
                assignControls();
            }  
        }
	  
        private void OnEnable() 
        {
            for(int i=0; i<_levelHolder.transform.childCount; ++i)
            {
                Tamagotchi aux = _levelHolder.transform.GetChild(i).gameObject.GetComponent<Tamagotchi>();
                _levels.Add(aux);
                aux.SetUnlocked(i <= currentLevelIndex);
                aux.SetTamagotchi(hasTamagotchi[i]);
            }

            _selectedIndex = 0;   

            if(!_loadedControls)
            {
                assignControls();
            }       
        }

        private void OnDisable() 
        {
            _scrollControls.started -= processScroll;
            _scrollControls.Disable();

            _selectControls.started -= selectLevel; 
            _selectControls.Disable();

            _selectControls.started -= CancelInput; 
            _selectControls.Disable();
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
                _scrollControls = _controlManager.Controls.UI.Navigate;
                _scrollControls.started += processScroll;
                _scrollControls.Enable();

                _selectControls = _controlManager.Controls.UI.Select;
                _selectControls.started += selectLevel; 
                _selectControls.Enable();

                _cancelControls = _controlManager.Controls.UI.Cancel;
                _cancelControls.started += CancelInput; 
                _cancelControls.Enable();

                _loadedControls = true;
            }
        }

        private void processScroll(InputAction.CallbackContext context)
        {
            _controlManager.CheckScheme(context.control.device.name);
            float value = context.ReadValue<Vector2>().x;
            if(value > 0)
            {
                ExecuteEvents.Execute(RightButton.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
            else
            {
                ExecuteEvents.Execute(LeftButton.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
        }

        public void StartScrollCoroutine(float value)
        {
            if(_scrollCoroutine == null)
            {
                _scrollCoroutine = StartCoroutine(crScroll(value));
            }
        }

        private void selectLevel(InputAction.CallbackContext context)
        {
            _controlManager.CheckScheme(context.control.device.name);
            if(_selectedIndex <= currentLevelIndex) // _selectedIndex <= CURRENTLEVELINDEX
            {
                AkSoundEngine.PostEvent("Entrar_nivel", this.gameObject);
                _gameFlowController.LoadScene(_levels[_selectedIndex].gameObject.name);
            }
            else
            {
                Debug.Log("Nivel no desbloqueado.");
            }
        }

        private void CancelInput(InputAction.CallbackContext context)
        {
            if(gameObject != null || gameObject.active) AkSoundEngine.StopAll();
            print("vaya");
            _controlManager.CheckScheme(context.control.device.name);
            _gameFlowController.LoadScene("MainMenu");
        }

        private IEnumerator crScroll(float value)
        {
            AkSoundEngine.PostEvent("Click", this.gameObject);
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
