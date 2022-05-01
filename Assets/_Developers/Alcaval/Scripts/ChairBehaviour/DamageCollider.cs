using System.Collections;
using System.Collections.Generic;
using TFMEsada;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    #region Fields

    [SerializeField] private Collider _hitbox;

    #endregion

    #region Life Cycle

    private void Awake() {
        //_hitbox = GetComponent<Collider>();
        _hitbox.isTrigger = true;
        _hitbox.enabled = false;
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            Debug.Log(other.name);
            other.gameObject.GetComponent<ControlManager>().PlayerDeath();
        }
    }

    #endregion
}
