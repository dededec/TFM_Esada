using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    /// <summary>  
	/// Attack state in stateMachine used when agent close enough to player and able to attack
	/// </summary>
    public class AttackState : State
    {
        #region Fields

        // States: Here you can have all the states that this state
        //         state could go to
        [SerializeField] private IdleState _idleState;
        [SerializeField] private RunState _runState;
        [SerializeField] private Animator animator;

        // Flag that controls whether you have finished an attack or not
        public bool finishedAttacking;

        // Cooldown for the attack
        [SerializeField] private float _cooldown = 2f;
        
        // Flag if you are able to attack
        private bool _ableToAttack = true;

        // HitBox manager, we have to enable the hitbox when the sweetspot of the animation is active so it can hit the player
        [SerializeField] private DamageCollider _hitbox;
        [SerializeField] private ChairBehaviour chairBehaviour;

        private GameObject player;

        #endregion

        private void Awake() {
            player = GameObject.Find("Player");
        }

        #region Public Methods
        public override State RunCurrentState()
        {
            if(player == null)
            {
                _idleState.inRangePlayer = false;
                return _idleState;
            }
            else if(finishedAttacking)
            {
                finishedAttacking = false;
                //chairBehaviour.playAwake();
                return _runState;
            } 

            if(!_hitbox.playerDead)
            {
                StartCoroutine(AttackCoroutine(0f));
                StartCoroutine(startCooldownRoutine());    
            }
            return this;
        }

        #endregion

        #region Private Methods
        
        #endregion

        #region Coroutines

        IEnumerator AttackCoroutine(float s)
        {
            if(_ableToAttack) 
            {
                yield return new WaitForSeconds(s);
                //AkSoundEngine.StopAll(animator.gameObject);
                animator.SetTrigger("attack");
                //AkSoundEngine.PostEvent("silla_atacando", animator.gameObject); // IGUAL TENEMOS QUE SONORIZAR DESDE ANIMACI�N
                yield return new WaitForSeconds(1f);
                _ableToAttack = false;
            }
        }

        IEnumerator startCooldownRoutine(){
            animator.SetTrigger("awake");
            //AkSoundEngine.StopAll(animator.gameObject);
            //AkSoundEngine.PostEvent("silla_despierta", animator.gameObject); 
            yield return new WaitForSeconds(_cooldown);
            _ableToAttack = true;
            finishedAttacking = true;
        }

        #endregion
    }
}