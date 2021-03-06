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
        int a = 0;
        foreach(GameObject g in chairs)
        {
            if(g != null) a++;
        }
        if(numOfChairsAwake <= 0 || a == 0) AkSoundEngine.StopPlayingID(idWalkSound);
    }

    public void bookAwake()
    {
        numOfBooksAwake++;
        print("Antes" + numOfBooksAwake);
        if(numOfBooksAwake == 1)
        {
            print("durante");
            idBookSound = AkSoundEngine.PostEvent("libro_despierto", gameObject);
            print(AkSoundEngine.GetEventIDFromPlayingID(idBookSound));
        } 
        print("Despues" + numOfBooksAwake);
    }

    public void bookStop()
    {

        numOfBooksAwake--;
        if(numOfChairsAwake <= 0) AkSoundEngine.StopPlayingID(idBookSound); print("ah");
    }

    public void pauseAnySound()
    {
        AkSoundEngine.Suspend();
    }
}
