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
    public class VolumeManager : MonoBehaviour
    {
        private const float MaxMusicVolume = 50f;
        private const float MaxSFXVolume = 50f;
        private const float Increment = 5f;

        [SerializeField] private Image _musicSlider;
        [SerializeField] private Image _SFXSlider;

        [Header("Volume settings")]
        private static float _musicVolume = 25f;
        private static float _SFXVolume = 25f;


        // private int type = 1;

        private void OnEnable()
        {
            _musicSlider.fillAmount = _musicVolume / MaxMusicVolume;
            _SFXSlider.fillAmount = _SFXVolume / MaxSFXVolume;
        }

        public void IncreaseMusic()
        {
            if (_musicVolume >= MaxMusicVolume) return;
            if (_musicVolume + Increment > MaxMusicVolume)
            {
                _musicVolume = MaxMusicVolume;
            }
            else
            {
                _musicVolume += Increment;
            }

            AkSoundEngine.SetRTPCValue("Music_Volume", _musicVolume);
            _musicSlider.fillAmount = _musicVolume / MaxMusicVolume;
        }

        public void DecreaseMusic()
        {
            if (_musicVolume <= 0f) return;
            if (_musicVolume - Increment < 0f)
            {
                _musicVolume = 0f;
            }
            else
            {
                _musicVolume -= Increment;
            }

            AkSoundEngine.SetRTPCValue("Music_Volume", _musicVolume);
            _musicSlider.fillAmount = _musicVolume / MaxMusicVolume;
        }

        public void IncreaseSFX()
        {
            if (_SFXVolume >= MaxSFXVolume) return;
            if (_SFXVolume + Increment > MaxSFXVolume)
            {
                _SFXVolume = MaxSFXVolume;
            }
            else
            {
                _SFXVolume += Increment;
            }

            AkSoundEngine.SetRTPCValue("SFX_Volume", _SFXVolume);
            _SFXSlider.fillAmount = _SFXVolume / MaxSFXVolume;
        }

        public void DecreaseSFX()
        {
            if (_SFXVolume <= 0f) return;
            if (_SFXVolume - Increment < 0f)
            {
                _SFXVolume = 0f;
            }
            else
            {
                _SFXVolume -= Increment;
            }

            AkSoundEngine.SetRTPCValue("SFX_Volume", _SFXVolume);
            _SFXSlider.fillAmount = _SFXVolume / MaxSFXVolume;
        }
    }
}
