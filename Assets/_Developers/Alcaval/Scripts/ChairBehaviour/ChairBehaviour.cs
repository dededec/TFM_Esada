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

        // Current state in the stateMachine
        public State _currentState;
        [SerializeField] private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private bool dead = false;
        public uint idWalkSound;
        private GameObject _player;
	  
	    #endregion
	  
	    #region Properties
	  
        // public string Ejemplo {get; set;}
            
	    #endregion
	 
	    #region LifeCycle
        
        private void Awake() {
            GameStateManager.instance.onGameStateChanged += onGameStateChanged;
            _player = GameObject.Find("Player");
        }

        private void OnDestroy()
        {
            GameStateManager.instance.onGameStateChanged -= onGameStateChanged;
        }

        private void Start() 
        {
            _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        }

        void Update() 
        {
            RunStateMachine();
            if(_player.GetComponent<ControlManager>().PlayerDead)
            {
                changeState(gameObject.GetComponentInChildren<IdleState>());
            }
        }
      
        #endregion

        #region Private Methods
	    
        // Runs the stateMachine constantly so the agent, turn off to stop agent
        private void RunStateMachine()
        {
            State newState = _currentState?.RunCurrentState();

            if(newState != null){
                changeState(newState);
            }
        }

        // Assigns the new state of the stateMachine to the current one
        private void changeState(State newState)
        {
            _currentState = newState;
        }

        #endregion

        #region Public Methods

        public void playAwake()
        {
            gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().chairAwake();
            //idWalkSound = AkSoundEngine.PostEvent("silla_despierta", gameObject);
        }

        public void stopAwake()
        {
            gameObject.transform.parent.gameObject.GetComponent<EnemyAudioController>().chairStop();
            //AkSoundEngine.StopPlayingID(idWalkSound);
        }

        public void death()
        {
            stopAwake();
            dead = true;
            AkSoundEngine.PostEvent("silla_defeated", gameObject);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.GetComponent<ChairBehaviour>().enabled = false;
            _navMeshAgent.isStopped = true;
            _animator.SetBool("falling", true);
            //Destroy(gameObject);
        }

        public void fall()
        {
            // _animator.ResetTrigger("up");
            // _animator.SetTrigger("fall"); 
        }

        public void Attack()
        {
            AkSoundEngine.PostEvent("silla_atacando", gameObject);
            if(GetComponentInChildren<DamageCollider>() != null) GetComponentInChildren<DamageCollider>()._hitbox.enabled = true;
        }

         public void NotAttack()
        {
            GetComponentInChildren<DamageCollider>()._hitbox.enabled = false;
        }

        public void playFront()
        {
            AkSoundEngine.PostEvent("silla_pataDelantera", gameObject);
        }

        public void playBack()
        {
            AkSoundEngine.PostEvent("silla_pataTrasera", gameObject);
        }


            
        #endregion

        public bool pausedCoroutines = false;
        Vector3 _pausedVelocity;
        Vector3 _pausedAngularVelocity; 

        public void pauseChair()
        {
            gameObject.GetComponent<Animator>().speed = 0;
            pausedCoroutines = true;
            _navMeshAgent.isStopped = true;
        }

        public void resumeChair()
        {
            gameObject.GetComponent<Animator>().speed = 1;
            pausedCoroutines = false;
            if(!dead) _navMeshAgent.isStopped = false;
        }

        private void onGameStateChanged(GameState newGameState)
        {
            switch (GameStateManager.instance.CurrentGameState)
            {
                case GameState.Gameplay:
                    resumeChair();
                    break;
                case GameState.Paused:
                    pauseChair();
                    break;
                default:
                    break;
            }
        }
    }
}