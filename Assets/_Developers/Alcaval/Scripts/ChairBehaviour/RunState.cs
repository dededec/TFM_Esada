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
        private AttackState _attackState;

        // Variable that lets us and the script know if the agent is inAttackRange of the player
        public bool inAttackRangePlayer{set; get;}

        // NavMeshAgent of the chair
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region LifeCycle

        private void Start() {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
            _attackState = gameObject.transform.parent.GetComponentInChildren<AttackState>();
        }
      
        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {   
            if(inAttackRangePlayer){
                _navMeshAgent.isStopped = true;
                return _attackState;
            }else{
                _navMeshAgent.destination = _playerPos.transform.position;
                return this;
            }
        }

        #endregion

        #region Private Methods
        
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
                inAttackRangePlayer = true;
            }
        }

        #endregion
    }
}