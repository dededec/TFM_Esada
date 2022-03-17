using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TFMEsada
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class NoteController : MonoBehaviour
    {
        #region Fields
      
        private GameObject _speechBubble;
        private TMP_Text _text;

        

        [Tooltip("Contents of the note no more than n characters")]
        [SerializeField] private string[] _contentOfNote;
	  
	    #endregion
	 
	    #region LifeCycle
	  
        private void OnEnable() {
            _speechBubble = GameObject.FindGameObjectWithTag("Bubble");
        }
      
        #endregion

	    #region Public Methods


	   
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
                _speechBubble.transform.GetChild(0).gameObject.SetActive(true);
                _text =  _speechBubble.GetComponentInChildren<TMP_Text>();
            }
        }
	   
        #endregion
    }
}