using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "light") other.gameObject.GetComponentInChildren<Light>().enabled = true;   
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "light") other.gameObject.GetComponentInChildren<Light>().enabled = false;   
    }
}
