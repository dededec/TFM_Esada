using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TFMEsada;

public class TaskAttack : Node
{
    public bool isAttacking = true;
    public bool inAttack = false;

    private GameObject _book;
    private GameObject _player;
    private float _force;
    
    public TaskAttack(GameObject book, GameObject player, float force)
    {
        this._book = book;
        this._player = player;
        this._force = force;
    }

    public override NodeState Evaluate()
    {
        if(isAttacking)
        {
            _book.GetComponent<Animator>().SetBool("attacking", true);
            _book.GetComponent<Rigidbody>().useGravity = true;
            Vector3 playerPosModified = new Vector3(_player.transform.position.x, _player.transform.position.y + 3, _player.transform.position.z);
            AkSoundEngine.StopAll(_book);


            _book.GetComponent<Rigidbody>().velocity = (playerPosModified - _book.transform.position) * _force;
            inAttack = true;
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
