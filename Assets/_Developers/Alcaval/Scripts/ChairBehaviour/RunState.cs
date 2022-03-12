using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TFMEsada
{
    /// <summary>  
	/// Run state in stateMachine, used when agent is moving to player
	/// </summary>
    public class RunState : State
    {
        #region Fields
        
        // Transform that stores the player position
        [SerializeField] private Transform _playerPos;

        // States: Here you can have all the states that this state state could go to
        [SerializeField] private AttackState _attackState;

        // Transform of the chair so it always rotates to the direction of the player when moving
        [SerializeField] private Transform _chairTransform;

        // Variable that lets us and the script know if the agent is inAttackRange of the player
        public bool inAttackRangePlayer{set; get;}

        // NavMeshAgent of the chair
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region LifeCycle

        private void Start() 
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        }
      
        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {   
            if(inAttackRangePlayer)
            {
                _navMeshAgent.isStopped = true;
                return _attackState;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _chairTransform.LookAt(_playerPos.transform);
                _navMeshAgent.destination = _playerPos.transform.position;
                return this;
            }
        }

        #endregion

        #region Private Methods
        
        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                inAttackRangePlayer = true;
            }
        }

        private void OnTriggerExit(Collider other) 
        {
            if(other.tag == "Player")
            {
                print("se salio o murio");
                inAttackRangePlayer = false;
            }
        }

        #endregion
    }
}