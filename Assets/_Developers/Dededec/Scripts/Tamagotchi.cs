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
    public class Tamagotchi : MonoBehaviour
    {
        #region Fields
      
        [SerializeField] private Texture2D _lockedTexture;
        [SerializeField] private Texture2D _unlockedTexture;
        [SerializeField] private Renderer _bodyRenderer;
        [SerializeField] private Renderer _buttonRenderer;
        [SerializeField] private Renderer _screenRenderer;
	  
	    #endregion
	 
	    #region LifeCycle

        public void SetUnlocked(bool unlocked)
        {
            if(unlocked)
            {
                _screenRenderer.material.mainTexture = _unlockedTexture;
            }
            else
            {
                _screenRenderer.material.mainTexture = _lockedTexture;
            }
        }

        public void SetTamagotchi(bool hasTamagotchi)
        {
            if(hasTamagotchi)
            {
                _buttonRenderer.sharedMaterial.SetFloat("_Opacity", 1);
                _bodyRenderer.sharedMaterial.SetFloat("_Opacity", 1);
            }
            else
            {
                _buttonRenderer.sharedMaterial.SetFloat("_Opacity", 0.3f);
                _bodyRenderer.sharedMaterial.SetFloat("_Opacity", 0.3f);
            }
        }
      
        #endregion
    }
}
