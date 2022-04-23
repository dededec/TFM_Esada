using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class CheckInAttackRange : Node
{
    private Transform _bookPos;
    private Transform _playerPos;
    private float _rangeAttackBook;

    public CheckInAttackRange(Transform bookPos, Transform playerPos, float rangeAttackBook)
    {
        this._bookPos = bookPos;
        this._playerPos = playerPos;
        this._rangeAttackBook = rangeAttackBook;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
 
        Debug.DrawRay(_bookPos.position, (_playerPos.position - _bookPos.position), Color.green);
 
        if(Physics.Raycast(_bookPos.position,(_playerPos.position - _bookPos.position) * 1, out hit) && hit.transform.tag != "Player") 
        {
            Debug.DrawLine (_bookPos.position, hit.point, Color.red);
        }

        if(hit.transform.gameObject.tag == "Player" && Vector3.Distance(_bookPos.position, _playerPos.position) < _rangeAttackBook) 
        {
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
