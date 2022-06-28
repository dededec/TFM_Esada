using System.Collections;
using System.Collections.Generic;
using TFMEsada;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    #region Fields

    [SerializeField] public Collider _hitbox;
    public bool playerDead = false;
    [SerializeField] private Animator _animator;
    private GameObject _player;

    #endregion

    #region Life Cycle

    private void Awake() {
        _hitbox.isTrigger = true;
        _hitbox.enabled = false;
        _player = GameObject.Find("Player");
    }

    #endregion

    #region Private Methods

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && !_player.GetComponent<ControlManager>().PlayerDead)
        {
            other.gameObject.GetComponent<ControlManager>().PlayerDeath();
            playerDead = true;
            _animator.SetTrigger("playerDead");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    #endregion
}
