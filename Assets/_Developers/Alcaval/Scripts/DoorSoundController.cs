using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundController : MonoBehaviour
{
    public void OnDoorClose()
    {
        AkSoundEngine.PostEvent("cerrar_puerta", this.gameObject);
    }
}
