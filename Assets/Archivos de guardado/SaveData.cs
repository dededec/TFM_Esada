using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    /*
    ¿Qué hay que guardar?
    1.- Último nivel alcanzado (nivel a pasar) -> int
    2.- Tamagotchis obtenidos -> bool[]
    */
    public int currentLevelIndex;
    public bool[] hasTamagotchi;

    public SaveData()
    {
        currentLevelIndex = 0;
        hasTamagotchi = new bool[3];
        for(int i=0; i<3; ++i)
        {
            hasTamagotchi[i] = false;
        }
    }
}
