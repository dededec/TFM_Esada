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
        [SerializeField] private FallState _fallState;

        // Transform of the chair so it always rotates to the direction of the player when moving
        [SerializeField] private Transform _chairTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform sillaMesh;

        // Variable that lets us and the script know if the agent is inAttackRange of the player
        public bool inAttackRangePlayer{set; get;}

        // NavMeshAgent of the chair
        private NavMeshAgent _navMeshAgent;

        public bool fell = false;
        public bool justUp = false;
        

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
            // if(justUp)
            // {
            //     // _animator.SetTrigger("up");
            //     // justUp = false;
            // }

            if(fell)
            {
                // _fallState.up = false;
                // _fallState.fallStart = true;
                // _navMeshAgent.isStopped = true;
                // return _fallState;
                fell = false;
                StartCoroutine(FallCoroutine(5f));
                return this;
            }
            else if(currentlyFalling)
            {
                _navMeshAgent.isStopped = true;
                return this;
            }
            else if(GameObject.FindGameObjectWithTag("Player") == null)
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

        #region Public methods

        public void fall()
        {
            print("se choco con el charco con " + canFall);
            if(canFall)
            {
                //print("Animacion de caida, desde run");
                fell = true; 
                //_animator.SetTrigger("fall"); 
                canFall = false;
            }
        }
            
        #endregion


        public bool currentlyFalling = false;
        public bool canFall = true;

        #region Coroutines

        IEnumerator FallCoroutine(float s)
        {
            //_animator.SetTrigger("fall");
            currentlyFalling = true;
            _animator.SetBool("falling", currentlyFalling);
            yield return new WaitForSeconds(s);
            // _animator.ResetTrigger("fall");
            StartCoroutine(UpCoroutine());
        }

        IEnumerator UpCoroutine()
        {
            //_animator.SetTrigger("up");
            currentlyFalling = false;
            _animator.SetBool("falling", currentlyFalling);
            yield return new WaitForSeconds(0.3f);
            _animator.ResetTrigger("up");
            yield return new WaitForSeconds(3f);
            canFall = true;
        }

        #endregion
    }
}