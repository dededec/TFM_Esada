using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TFMEsada;

public class TaskAttack : Node
{
    public bool isAttacking = true;

    private GameObject _book;
    private GameObject _player;
    private GameObject _trail;
    private float _force;
    public TaskAttack(GameObject book, GameObject player, float force, GameObject trail)
    {
        this._book = book;
        this._player = player;
        this._force = force;
        this._trail = trail;
    }

    public override NodeState Evaluate()
    {
        if(isAttacking)
        {
            _trail.gameObject.GetComponent<TrailRenderer>().emitting = true;
            _book.GetComponent<Rigidbody>().useGravity = true;
            _book.GetComponent<Rigidbody>().velocity = (_player.transform.position - _book.transform.position) * _force;
            isAttacking = false;
        }
        

        state = NodeState.RUNNING;
        return state;
    }

    public void setAttack(bool state)
    {
        isAttacking = state;
    }
}
