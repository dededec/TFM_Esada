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
        private GameObject _player;

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

        private ChairBehaviour cb;
        

        #endregion

        #region LifeCycle

        private void Start() 
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
            _playerPos = GameObject.Find("Player").gameObject.transform;
            _player = _playerPos.gameObject;
            cb = gameObject.transform.parent.parent.gameObject.GetComponent<ChairBehaviour>();
        }
      
        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {   
            if(fell)
            {
                fell = false;
                StartCoroutine(FallCoroutine(5f));
                return this;
            }
            else if(currentlyFalling)
            {
                _navMeshAgent.isStopped = true;
                return this;
            }
            else if(_player == null)
            {
                _idleState.inRangePlayer = false;
                return _idleState;
            }
            else if(inAttackRangePlayer && !cb.pausedCoroutines)
            {
                _navMeshAgent.isStopped = true;
                //_chairTransform.gameObject.GetComponent<ChairBehaviour>().stopAwake();
                return _attackState;
            }
            else
            {
                if(!cb.pausedCoroutines)
                {
                    _navMeshAgent.isStopped = false;
                    _chairTransform.LookAt(_playerPos.transform);
                    _navMeshAgent.destination = _playerPos.transform.position;
                }
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
            if(canFall)
            {
                fell = true; 
                canFall = false;
            }
        }
            
        #endregion


        public bool currentlyFalling = false;
        public bool canFall = true;

        #region Coroutines

        IEnumerator FallCoroutine(float s)
        {
            currentlyFalling = true;
            //ESTA SERIA LA LINEA QUE HAY QUE DESCOMENTAR PARA QUE SUENE EL RESBALARSE
            AkSoundEngine.PostEvent("silla_resbalando", gameObject);
            _animator.SetBool("falling", currentlyFalling);

            float timer = 0f;
            while(timer < s) 
            {
                if(cb.pausedCoroutines)
                    yield return null;
                else
                    timer += Time.deltaTime;
                yield return null;
            }

            //yield return new WaitForSeconds(s);
            currentlyFalling = false;
            _animator.SetBool("falling", currentlyFalling);
            StartCoroutine(UpCoroutine());
        }

        IEnumerator UpCoroutine()
        {
            //_animator.SetTrigger("up");
            float timer = 0f;
            while(timer < 0.3f) 
            {
                if(cb.pausedCoroutines)
                    yield return null;
                else
                    timer += Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(0.3f);
            _animator.ResetTrigger("up");
            timer = 0f;
            while(timer < 3f) 
            {
                if(cb.pausedCoroutines)
                    yield return null;
                else
                    timer += Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(3f);
            canFall = true;
        }

        #endregion
    }
}