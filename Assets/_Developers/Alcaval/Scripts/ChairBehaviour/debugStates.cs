using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
{
    public class debugStates : MonoBehaviour
    {
        [SerializeField] private IdleState idleState;
        public bool RangeIDLE = false;

        [SerializeField] private RunState runState;
        public bool inAttackRangeRUN = false;

        [SerializeField] private AttackState attackState;
        public bool finishAttackATTACK = false;

        private void Update() {
            RangeIDLE = idleState.inRangePlayer;
            inAttackRangeRUN = runState.inAttackRangePlayer;
            finishAttackATTACK = attackState.finishedAttacking;
        }
    }
}