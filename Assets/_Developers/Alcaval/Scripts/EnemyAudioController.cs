using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    List<GameObject> chairs = new List<GameObject>();
    int numOfChairsAwake = 0;
    public uint idWalkSound;
    List<GameObject> books = new List<GameObject>();
    public int numOfBooksAwake = 0;
    public uint idBookSound;

    private void Start() {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if(child.name[0].ToString() == "C")
                chairs.Add(child);
            else
                books.Add(child);
        }
    }

    public void chairAwake()
    {
        numOfChairsAwake++;
        if(numOfChairsAwake == 1)
            idWalkSound = AkSoundEngine.PostEvent("silla_despierta", gameObject);
    }

    public void chairStop()
    {
        numOfChairsAwake--;
        if(numOfChairsAwake <= 0) AkSoundEngine.StopPlayingID(idWalkSound);
    }

    public void bookAwake()
    {
        //print("doesnt make sense");
        numOfBooksAwake++;
        if(numOfBooksAwake == 1)
            idBookSound = AkSoundEngine.PostEvent("libro_despierto", gameObject);
    }

    public void bookStop()
    {
        numOfBooksAwake--;
        if(numOfChairsAwake <= 0) AkSoundEngine.StopPlayingID(idBookSound);
    }

    public void pauseAnySound()
    {
        AkSoundEngine.Suspend();
    }
}
