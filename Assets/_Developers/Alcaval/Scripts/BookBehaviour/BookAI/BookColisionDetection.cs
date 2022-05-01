using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookColisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private BookBehaviourTree bbt;

    private bool _recolocate = false;
    private bool _flapIdle = false;

    float x = 0f;
    float y = 0f;

    private void Update() {
        Vector3 targetPosition = Vector3.forward;

        if(_flapIdle)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2, transform.position.z), 2 * Time.deltaTime);
            this.transform.LookAt(targetPosition);
        }
        else if(_player != null)
        {
            if(_recolocate)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 2.5f, _player.transform.position.z), 3 * Time.deltaTime);
            }

            if(_player != null){
                targetPosition = new Vector3( _player.transform.position.x, this.transform.position.y, _player.transform.position.z);
                this.transform.LookAt(targetPosition);
            } 
        }
        else
        {
            _flapIdle = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Player" && bbt.taskAttack.isAttacking == false)
        {
            Destroy(other.gameObject);
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
            _flapIdle = true;
        }

        if(other.transform.tag == "Note")
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
        }
    }

    IEnumerator cooldownCoroutine()
    {
        if(!_flapIdle) transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(4f);

        if(!_flapIdle)
        {
            bbt.taskAttack.setAttack(true);
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", false);
            _recolocate = false;
        }

    }
}
