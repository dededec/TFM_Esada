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

        #endregion

        #region Public Methods
        public override State RunCurrentState()
        {
            if(GameObject.FindGameObjectWithTag("Player") == null)
            {
                _idleState.inRangePlayer = false;
                return _idleState;
            }
            else if(finishedAttacking)
            {
                finishedAttacking = false;
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
                animator.SetTrigger("attack");
                yield return new WaitForSeconds(1f);
                _ableToAttack = false;
            }
        }

        IEnumerator startCooldownRoutine(){
            yield return new WaitForSeconds(_cooldown);
            _ableToAttack = true;
            finishedAttacking = true;
        }

        #endregion
    }
}