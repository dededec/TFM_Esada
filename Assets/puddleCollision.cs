using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class puddleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponentInChildren<RunState>().fall();
        }
    }
}
