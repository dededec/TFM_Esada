using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookColisionDetection : MonoBehaviour
{
    [SerializeField] BookBehaviourTree bbt;
    private bool _recolocate = false;
    float x = 0f;
    float y = 0f;

    private void Update() {
        Debug.Log(bbt.taskAttack.isAttacking);

        if(_recolocate)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(x, 2, y), 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other) {
        print("chocado con algo");
        if(other.transform.tag == "Player")
        {
            Debug.Log("Get chocado");
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
        }

        if(other.transform.tag == "Note")
        {
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
