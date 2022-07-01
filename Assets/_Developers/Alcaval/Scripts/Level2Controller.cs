using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TFMEsada;

public class Level2Controller : MonoBehaviour
{
    [SerializeField] private GameObject _fakeWall;

    private int phoneState = 0;
    [SerializeField] private GameObject[] phoneNotes;

    private int tamagochiState = 0;
    [SerializeField] private GameObject[] tamagochiNotes;
    [SerializeField] private GameObject tamagochi;
    [SerializeField] private Transform[] floorSpawnPoints;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private GameObject player;
    
    private GameObject _bubble;

    private void Awake() {
        _bubble = GameObject.Find("Bubble");
    }

    public void UpdatePhone()
    {
        Debug.Log("SE UPDATEA PHONE");
        phoneState++;
        if(phoneState == 1)
        {
            phoneNotes[0].gameObject.SetActive(false);
            phoneNotes[0].gameObject.SetActive(true);
            phoneNotes[1].gameObject.SetActive(false);
        }
        if(phoneState == 2)
        {
            phoneNotes[0].gameObject.SetActive(false);
            phoneNotes[0].gameObject.SetActive(false);
            phoneNotes[1].gameObject.SetActive(true);
        }
        if(phoneState == 3)
        {
            OpenSecretRoom();
        }
    }

    public void UpdateTamagochiQuest()
    {
        if(tamagochiState == 0)
        {
            foreach(GameObject go in tamagochiNotes)
            {
                go.SetActive(true);
            }
        }

        if(tamagochiState == 1)
        {
            foreach(GameObject go in tamagochiNotes)
            {
                go.SetActive(false);
            }
            tamagochi.SetActive(true);
        }

        tamagochiState++;
    }

    public void OpenSecretRoom()
    {
        _fakeWall.transform.position = new Vector3(_fakeWall.transform.position.x, -10f, _fakeWall.transform.position.z);
    }

    public void TeleportPlayer(int floor)
    {   
        player.transform.position = floorSpawnPoints[floor].position;
        if(floor == 0)
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
            _bubble.GetComponent<SpeechBublleController>()._renderCamera = cameras[0].GetComponent<Camera>();
            player.transform.rotation *= Quaternion.AngleAxis( 180, transform.up); 
        }
        else
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            _bubble.GetComponent<SpeechBublleController>()._renderCamera = cameras[1].GetComponent<Camera>();
            player.transform.rotation *= Quaternion.AngleAxis( 180, transform.up); 
        }
    }
}
