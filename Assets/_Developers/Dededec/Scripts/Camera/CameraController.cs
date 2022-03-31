using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TFMEsada
{
    /// <summary>  
	/// Controls rotation of the camera
	/// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Fields

        [Tooltip("Pivot point for the rotation of the camera")]
        /// <summary>
        /// Pivot point for the rotation of the camera
        /// </summary>
        [SerializeField] private Transform _pivotPoint;

        [Tooltip("PixelatedCamera script used for giving the game its look.")]
        /// <summary>
        /// PixelatedCamera script used for giving the game its look.
        /// </summary>
        [SerializeField] private PixelatedCamera _cameraOptions;

        [Tooltip("ControlManager script to assign controls to this script.")]
        /// <summary>
        /// Movement script to assign controls to this script.
        /// </summary>
        [SerializeField] private ControlManager _controlManager;
        private InputAction _look;
        private InputAction _lockScreen;

        private string _mouseDeltaName = "delta";
        private bool _isMouse;
        private bool _isRightButtonPressed = false;

        private float _distanceToPivot;
        private float _originalY;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            if (_cameraOptions != null)
            {
                _cameraOptions.mode = PixelatedCamera.PixelScreenMode.Scale;
                _cameraOptions.screenScaleFactor = 4;
            }

            _distanceToPivot = Vector3.Distance(transform.position, _pivotPoint.position);
            _originalY = transform.position.y;
        }

        private void OnEnable()
        {
            if(_controlManager == null)
            {
                _controlManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlManager>();
            }
            assignControls();
        }

        private void OnDisable()
        {
            _look.started -= determineControl;
            _lockScreen.started -= onRightButtonPress;
            _lockScreen.canceled -= onRightButtonRelease;
            
            _look.Disable();
            _lockScreen.Disable();
        }

        /*
        Note: I use Start() instead of OnEnable() because it is NOT guaranteed that
        this script's OnEnable() function will execute BEFORE ControlManager's Awake() function.
        For reference: https://forum.unity.com/threads/onenable-before-awake.361429/
        */
        private void Start()
        {
            assignControls();
        }

        void Update()
        {
            // La cámara está fixeada en la y
            // Rota alrededor del centro del escenario
            // Se hace zoom con rueda del ratón

            var cameraMovement = _look.ReadValue<Vector2>();
            if (_isRightButtonPressed)
            {
                transform.RotateAround(_pivotPoint.position, Vector3.up, cameraMovement.x * 360f * Time.deltaTime);
            }
            else if (!_isMouse)
            {
                transform.RotateAround(_pivotPoint.position, Vector3.up, cameraMovement.x * 100f * Time.deltaTime);
            }

            Debug.DrawLine(transform.position, _pivotPoint.position, Color.cyan);
            transform.LookAt(_pivotPoint, Vector3.up);
            // Comprobamos que seguimos a la distancia correcta
            if (Vector3.Distance(transform.position, _pivotPoint.position) != _distanceToPivot)
            {
                transform.position = (transform.position - _pivotPoint.position).normalized * _distanceToPivot + _pivotPoint.position;
                transform.position = new Vector3(transform.position.x, _originalY, transform.position.z);
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
                _look = _controlManager.Controls.Camera.Look;
                _lockScreen = _controlManager.Controls.Camera.LockScreen;
                _look.started += determineControl;
                _look.Enable();


                _lockScreen.started += onRightButtonPress;
                _lockScreen.canceled += onRightButtonRelease;
                _lockScreen.Enable();
                return true;
            }
        }

        private void determineControl(InputAction.CallbackContext context)
        {
            _isMouse = context.control.name == _mouseDeltaName;
        }

        private void onRightButtonPress(InputAction.CallbackContext context)
        {
            _isRightButtonPressed = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void onRightButtonRelease(InputAction.CallbackContext context)
        {
            _isRightButtonPressed = false;
            Cursor.lockState = CursorLockMode.None;
        }

        #endregion
    }
}
