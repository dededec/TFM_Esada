using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TFMEsada;

public class TaskIdle : Node
{

    public TaskIdle()
    {
        
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }
}
