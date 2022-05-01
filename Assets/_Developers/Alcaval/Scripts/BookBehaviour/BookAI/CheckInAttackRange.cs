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
        this._playerPos = playerPos.gameObject.transform.parent.gameObject.transform;
        this._rangeAttackBook = rangeAttackBook;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
        Vector3 bookPosModified = new Vector3(_bookPos.position.x, _bookPos.position.y + 0.5f, _bookPos.position.z);
 
        if(_playerPos != null)
        {
            Debug.DrawRay(bookPosModified, (_playerPos.position - bookPosModified), Color.green);
    
            if(Physics.Raycast(bookPosModified,(_playerPos.position - bookPosModified) * 1, out hit) && hit.transform.tag != "Player") 
            {
                Debug.DrawLine (bookPosModified, hit.point, Color.red);
                if(_bookPos.gameObject.GetComponent<Animator>().GetBool("attacking"))
                {
                    _bookPos.gameObject.GetComponent<Animator>().SetBool("flap", true);
                }
            }
            
            Debug.Log(hit.transform.gameObject.tag);
            if(hit.transform.gameObject.tag == "Player" && Vector3.Distance(bookPosModified, _playerPos.position) < _rangeAttackBook) 
            {
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                if(_bookPos.gameObject.GetComponent<Animator>().GetBool("attacking"))
                {
                    _bookPos.gameObject.GetComponent<Animator>().SetBool("flap", true);
                }
                state = NodeState.FAILURE;
                return state;
            }
        }else{
            return NodeState.FAILURE;
        }
    }
}
