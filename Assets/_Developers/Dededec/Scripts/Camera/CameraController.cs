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

        private IAA_Player _playerControls;
        private InputAction _look;
        private InputAction _lockScreen;

        private string _mouseDeltaName = "delta";
        private bool _isMouse;
        private bool _isRightButtonPressed = false;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            _cameraOptions.mode = PixelatedCamera.PixelScreenMode.Scale;
            _cameraOptions.screenScaleFactor = 4;
            _playerControls = new IAA_Player();
        }

        private void OnEnable()
        {
            _look = _playerControls.Player.Look;
            _look.started += determineControl;
            _look.Enable();

            _lockScreen = _playerControls.Player.LockScreen;
            _lockScreen.started += onRightButtonPress;
            _lockScreen.canceled += onRightButtonRelease;
            _lockScreen.Enable();
        }

        private void OnDisable()
        {
            _look.Disable();
            _lockScreen.Disable();
        }

        void Update()
        {
            // La cámara está fixeada en la y
            // Rota alrededor del centro del escenario
            // Se hace zoom con rueda del ratón

            var cameraMovement = _look.ReadValue<Vector2>();
            if(_isRightButtonPressed)
            {
                transform.RotateAround(_pivotPoint.position, Vector3.up, cameraMovement.x * 360f * Time.deltaTime);
            }
            else if(!_isMouse)
            {
                transform.RotateAround(_pivotPoint.position, Vector3.up, cameraMovement.x * 100f * Time.deltaTime);
            }
        }

        #endregion

        #region Private Methods
        
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
