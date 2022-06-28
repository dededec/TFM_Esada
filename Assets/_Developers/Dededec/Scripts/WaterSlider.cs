/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class WaterSlider : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image[] _waterImages;
        private int _currentWater;
        [SerializeField] private RectTransform _waterTop;
	  
	    #endregion
	  
	    #region Properties

        public float Water
        {
            get
            {
                return _waterImages[_currentWater].fillAmount;
            }

            set
            {
                // StartCoroutine(crSetFill(value));
                _waterImages[_currentWater].fillAmount = value;
                if(_waterImages[_currentWater].fillAmount <= 0f && (_currentWater+1) < _waterImages.Length)
                {
                    _currentWater++;
                    
                    if(value < 0)
                    {
                        Water += value;           
                    }
                }
            }
        }
            
	    #endregion

        #region Private Methods

        private IEnumerator crSetFill(float value)
        {
            float duracion = 0.5f;
            float original = _waterImages[_currentWater].fillAmount;
            for(float i=0f; i<duracion; i+=Time.deltaTime)
            {
                _waterImages[_currentWater].fillAmount = Mathf.Lerp(original, value, i/duracion);
                yield return null;
            }
            _waterImages[_currentWater].fillAmount = value;
        }

        #endregion
    }
}
