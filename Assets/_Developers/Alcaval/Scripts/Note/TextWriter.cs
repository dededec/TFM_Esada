using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

namespace TFMEsada
{
    /// <summary>  
	/// This class writes letter by letter in the dialog given a speed
	/// </summary>
    public class TextWriter : MonoBehaviour
    {
        #region Fields

        /// <summary>  
	    /// The text in which we are going to write
	    /// </summary>
        private TMP_Text _text;
        
        /// <summary>  
	    /// The text we are going to write
	    /// </summary>
        private string _textToWrite;

        /// <summary>  
	    /// The index of the current letter that we have to write
	    /// </summary>

        private int _characterIndex;
        /// <summary>  
	    /// How fast it is written
	    /// </summary>
        private float _timePerCharacter;

        /// <summary>  
	    /// Timer total 
	    /// </summary>
        private float _timer;

        private bool wr = false;
	  
	    #endregion
	 
	    #region LifeCycle
	  
        private void Update() 
        {
            if(_text != null)
            {
                _timer -= Time.deltaTime;

                if(_timer <= 0f)
                {
                    _timer += _timePerCharacter;
                    _characterIndex++;
                    if(_characterIndex <= _textToWrite.Length)
                    {
                        wr = true;
                        _text.text = _textToWrite.Substring(0, _characterIndex);
                        Vector2 tSize = _text.GetRenderedValues(false);
                    } 

                    if(_characterIndex >= _textToWrite.Length)
                    {
                        transform.GetComponent<NoteController>().Writing = false;
                        _text = null;
                        wr = false;
                        return;
                    }  
                }
            }
        }
      
        #endregion

	    #region Public Methods

        public void AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter)
        {
            this._text = uiText;
            this._textToWrite = textToWrite;
            this._timePerCharacter = timePerCharacter;
            _characterIndex = 0;
        }

        public void FinishSentence()
        {
            _text.text = _textToWrite;
            _characterIndex = _textToWrite.Length;
        }
	   
        #endregion
    }
}