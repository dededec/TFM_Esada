using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TFMEsada
{
    /// <summary>  
	/// Class that controls the chair behaviour and shows where it has to move
	/// </summary>
    public class ChairBehaviour : MonoBehaviour
    {
        #region Fields

        // Transform that stores the player position
        [SerializeField] private Transform _playerPos;

        // NavMeshAgent of the chair
        private NavMeshAgent _navMeshAgent;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle

        private void Awake() {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            _navMeshAgent.destination = _playerPos.transform.position;
        }
      
        #endregion

	    #region Public Methods
	   
        #endregion

        #region Private Methods
	   
        #endregion
    }
}