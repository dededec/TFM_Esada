using System.Collections;
using System.Collections.Generic;
using TFMEsada;
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
                print("se deberia de recolocar");
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 3f, _player.transform.position.z), 3 * Time.deltaTime);
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
        // print(bbt.taskAttack.inAttack);
        // print(other.transform.tag);
        
        if(other.transform.tag == "Player" && bbt.taskAttack.inAttack)
        {
            AkSoundEngine.PostEvent("libro_chocando", gameObject);
            //AkSoundEngine.StopAll(transform.gameObject);
            // AQU� DEBER�A SONAR EL GOLPE CHOCANDO COSAS
            //AkSoundEngine.PostEvent("libro_chocando", transform.gameObject);
            other.gameObject.GetComponent<ControlManager>().PlayerDeath();
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().useGravity = false;

            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
            //AkSoundEngine.PostEvent("libro_despierto", transform.gameObject);

            _flapIdle = true;
            bbt.taskAttack.inAttack = false;
        }

        if(other.transform.tag == "Ground" && bbt.taskAttack.inAttack)
        {
            AkSoundEngine.PostEvent("libro_chocando", gameObject);
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
            StartCoroutine(cooldownCoroutine()); 
            _recolocate = true;
            x = transform.position.x;
            y = transform.position.y;
            bbt.taskAttack.inAttack = false;
        }
    }

    public void death()
    {
        Debug.Log("Se murio el libro");
        AkSoundEngine.PostEvent("libro_defeated", gameObject);
        Destroy(gameObject);
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


    public void PreAttack()
    {
        StartCoroutine(preAttack());
    }

    IEnumerator preAttack()
    {
        transform.GetComponent<Rigidbody>().useGravity = false;
        bbt.gameObject.GetComponent<Animator>().SetBool("flap", true);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, 2.5f, _player.transform.position.z), 0.5f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        bbt.checkInAttackRange.ready = true;
    }
}
