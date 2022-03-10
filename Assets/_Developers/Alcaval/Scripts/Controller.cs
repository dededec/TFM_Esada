using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class Controller : MonoBehaviour
    {
        private void Update() {
            Vector3 Movement = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += Movement * 10f * Time.deltaTime;
        }
    }
}