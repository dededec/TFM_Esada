/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public void OnFootStepEvent()
        {
            AkSoundEngine.PostEvent("Player_Footsteps", this.gameObject);
        }
    }
}
