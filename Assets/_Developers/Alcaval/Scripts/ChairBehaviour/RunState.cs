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
        private Transform _playerPos;

        // States: Here you can have all the states that this state state could go to
        [SerializeField] private AttackState _attackState;
        [SerializeField] private IdleState _idleState;

        // Transform of the chair so it always rotates to the direction of the player when moving
        [SerializeField] private Transform _chairTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform sillaMesh;

        // Variable that lets us and the script know if the agent is inAttackRange of the player
        public bool inAttackRangePlayer{set; get;}

        // NavMeshAgent of the chair
        private NavMeshAgent _navMeshAgent;
        

        #endregion

        #region LifeCycle

        private void Start() 
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
            _playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        }
      
        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {   
            //sillaMesh.transform.position = new Vector3(sillaMesh.position.x, -0.33f, sillaMesh.position.z);
            if(GameObject.FindGameObjectWithTag("Player") == null)
            {
                _idleState.inRangePlayer = false;
                //sillaMesh.transform.position = new Vector3(sillaMesh.position.x, 0.438f, sillaMesh.position.z);
                return _idleState;
            }
            else if(inAttackRangePlayer)
            {
                _navMeshAgent.isStopped = true;
                //sillaMesh.transform.position = new Vector3(sillaMesh.position.x, 0.438f, sillaMesh.position.z);
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
                inAttackRangePlayer = false;
            }
        }

        #endregion
    }
}