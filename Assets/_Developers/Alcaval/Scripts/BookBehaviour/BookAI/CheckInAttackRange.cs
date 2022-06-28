using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class CheckInAttackRange : Node
{
    private Transform _bookPos;
    private GameObject _player;
    private float _rangeAttackBook;
    public bool ready = false;
    private BookColisionDetection bookColisionDetection;
    private bool first = false;
    private TaskAttack ta;


    public CheckInAttackRange(TaskAttack ta, Transform bookPos, GameObject player, float rangeAttackBook, BookColisionDetection bookColisionDetection)
    {
        this._bookPos = bookPos;
        this._player = player;
        this._rangeAttackBook = rangeAttackBook;
        this.bookColisionDetection = bookColisionDetection;
        this.ta = ta;
    }

    public override NodeState Evaluate()
    {
        
        RaycastHit hit;
        Vector3 bookPosModified = new Vector3(_bookPos.position.x, _bookPos.position.y + 0.5f, _bookPos.position.z);
        Vector3 playerPosModified = new Vector3(_player.transform.position.x, _player.transform.position.y + 1.5f, _player.transform.position.z);
 
        if(_player != null)
        {
            Debug.DrawRay(bookPosModified, (playerPosModified - bookPosModified), Color.green);

            if(ready)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            if(Physics.Raycast(bookPosModified,(playerPosModified - bookPosModified) * 1, out hit) && hit.transform.tag != "Player") 
            {
                Debug.DrawLine (bookPosModified, hit.point, Color.red);
                if(_bookPos.gameObject.GetComponent<Animator>().GetBool("attacking"))
                {
                    _bookPos.gameObject.GetComponent<Animator>().SetBool("flap", true);
                }
            }
            
            if(hit.transform.gameObject.tag == "Player" && Vector3.Distance(bookPosModified, playerPosModified) < _rangeAttackBook && !first) 
            {
                if(!first)
                {
                    first = true;
                    Debug.Log("otra vez");
                    _bookPos.gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().bookAwake();
                    //AkSoundEngine.PostEvent("libro_despierto", _bookPos.gameObject); first = true;
                    bookColisionDetection.PreAttack();
                } 
                return state;
            }
            else if(ready)
            {
                state = NodeState.SUCCESS;
                ready = false;
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
