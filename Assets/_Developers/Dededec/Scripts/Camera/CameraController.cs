using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace TFMEsada
{
    /// <summary>  
	/// Controls rotation of the camera via Right Click + Mouse movement
	/// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _pivotPoint;
        
        #endregion

        #region LifeCycle

        void Update()
        {
            // La cámara está fixeada en la y
            // Rota alrededor del centro del escenario
            // Se hace zoom con rueda del ratón
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Confined;
                transform.RotateAround(_pivotPoint.position, Vector3.up, Input.GetAxis("Mouse X") * 720f * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(1))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        #endregion
    }
}
