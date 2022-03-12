using System.Collections;
using System.Collections.Generic;
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

    #region Public Methods
    
    public void enableHitbox()
    {
        _hitbox.enabled = true;
    }

    public void disableHitbox()
    {
        _hitbox.enabled = false;
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other) 
    {
        print("se encontro coas");
        if(other.tag == "Player")
        {
            print(other);
            Destroy(other.gameObject);
        }
    }

    #endregion
}
