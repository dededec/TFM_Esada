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
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class PauseMenuFunctions : MonoBehaviour
    {
        [SerializeField] private GameFlowController _gfc;

        public void Resume()
        {
            GameStateManager.instance.SetState(GameState.Gameplay);
        }

        public void ExitToMenu()
        {
            _gfc.LoadScene("LevelSelection");
        }
    }
}
