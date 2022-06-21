using System.Collections.Generic;
using TFMEsada;


public class BookBehaviourTree : Tree
{
    private UnityEngine.GameObject player;
    [UnityEngine.SerializeField] private float _rangeAttackBook;
    [UnityEngine.SerializeField] private float _force;
    [UnityEngine.SerializeField] private BookColisionDetection bookColisionDetection;

    public TaskAttack taskAttack;
    public CheckInAttackRange checkInAttackRange;

    private void Awake() {
        player = UnityEngine.GameObject.Find("Player");
        bookColisionDetection = gameObject.GetComponent<BookColisionDetection>();
    }

    protected override Node SetupTree()
    {
        taskAttack = new TaskAttack(transform.gameObject, player, _force);
        checkInAttackRange = new CheckInAttackRange(transform, player, _rangeAttackBook, bookColisionDetection);
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>{
                checkInAttackRange,
                taskAttack,
            }),
            new TaskIdle(),
        });
        return root;
    }
}