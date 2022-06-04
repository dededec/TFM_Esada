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
        [SerializeField] private bool _isLocked;

        public bool isLocked
        {
            get
            {
                return _isLocked;
            }

            set
            {
                _isLocked = value;
                SetVisibility();
            }
        }
	  
	    #endregion
	 
	    #region LifeCycle
	  
        private void Awake() 
        {
            SetVisibility();
        }

        private void SetVisibility()
        {
            if(_isLocked)
            {
                _screenRenderer.material.mainTexture = _lockedTexture;
                _buttonRenderer.sharedMaterial.SetFloat("_Opacity", 0.3f);
                _bodyRenderer.sharedMaterial.SetFloat("_Opacity", 0.3f);
            }
            else
            {
                _screenRenderer.material.mainTexture = _unlockedTexture;
                _buttonRenderer.sharedMaterial.SetFloat("_Opacity", 1);
                _bodyRenderer.sharedMaterial.SetFloat("_Opacity", 1);
            }
        }
      
        #endregion
    }
}
