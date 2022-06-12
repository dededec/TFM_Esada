using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class puddleCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Enemy")
        {
            other.collider.gameObject.GetComponentInChildren<FallState>().PuddleColission();
        }
    }
}
