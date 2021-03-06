using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class PuddleCollision : MonoBehaviour
{
    private GameObject _player;

    private void Start() 
    {
        _player = GameObject.Find("Player").transform.GetChild(5).gameObject;    
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy" && other.name[0].ToString() != "B")
        {
            other.GetComponent<ChairBehaviour>().fall();
            other.GetComponentInChildren<RunState>().fall();
        }
        else if(other.gameObject.tag == "Player")
        {
            AkSoundEngine.SetSwitch("Footsteps_Surface", "Charco", _player);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            AkSoundEngine.SetSwitch("Footsteps_Surface", "Normal", _player);
        }
    }
}
