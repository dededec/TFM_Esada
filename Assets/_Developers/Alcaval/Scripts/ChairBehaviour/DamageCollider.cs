using System.Collections;
using System.Collections.Generic;
using TFMEsada;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    #region Fields

    [SerializeField] private Collider _hitbox;
    public bool playerDead = false;

    #endregion

    #region Life Cycle

    private void Awake() {
        _hitbox.isTrigger = true;
        _hitbox.enabled = false;
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<ControlManager>().PlayerDeath();
            playerDead = true;
        }
    }

    #endregion
}
