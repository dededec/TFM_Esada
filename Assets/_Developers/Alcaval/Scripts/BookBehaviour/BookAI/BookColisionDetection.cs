using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookColisionDetection : MonoBehaviour
{
    [SerializeField] BookBehaviourTree bbt;
    private bool _recolocate = false;
    float x = 0f;
    float y = 0f;
    [SerializeField] private GameObject _player;

    private void Update() {
        if(_recolocate)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 2, _player.transform.position.z), 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.tag == "Player")
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().emitting = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
        }

        if(other.transform.tag == "Note")
        {
            transform.GetChild(0).GetComponent<TrailRenderer>().emitting = false;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
        }
        //StartCoroutine(cooldownCoroutine());  
    }

    IEnumerator cooldownCoroutine()
    {
        bbt.taskAttack.setAttack(false);
        // yield return new WaitForSeconds(1f);
        // _recolocate = true;   
        //transform.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, 2, transform.position.z));
        //transform.Translate(new Vector3(0, 2, 0), Space.World);
        transform.GetComponent<Rigidbody>().useGravity = false;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(3f);
        bbt.taskAttack.setAttack(true);
        _recolocate = false;

    }
}
