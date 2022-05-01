using System.Collections.Generic;
using TFMEsada;


public class BookBehaviourTree : Tree
{
    public UnityEngine.GameObject player;
    [UnityEngine.SerializeField] private float _rangeAttackBook;
    [UnityEngine.SerializeField] private float _force;

    public TaskAttack taskAttack;

    protected override Node SetupTree()
    {
        taskAttack = new TaskAttack(transform.gameObject, player, _force);
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>{
                new CheckInAttackRange(transform, player.transform, _rangeAttackBook),
                taskAttack,
            }),
            new TaskIdle(),
        });
        return root;
    }
}