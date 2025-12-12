using UnityEngine;

public class BeybladeMover : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject[] checkpoints;
    //[SerializeField] private float distanceSensibility = 0.1f;
    private TopDownMover2D mover;
    private EnemyDrop drop;
    private LifeController life;
    private Vector3 direction;
    private int index;

    private EnemiesAnimationHandler _enemyController;

    private void Awake()
    {
        drop = GetComponent<EnemyDrop>();
        life = GetComponent<LifeController>();
        mover = GetComponent<TopDownMover2D>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, checkpoints[index].transform.position);   //calcola la distanza tra la posizione dell'enemy e quella del checkpoint

        if (distance <= 0.1f)
        {
            index++;                                     //passa al prossimo checkpoint
            if (index >= checkpoints.Length) index = 0;  //quando raggiunge l'ultimo waypoint resetta, cos� da garantire un loop di movimento
        }

        direction = checkpoints[index].transform.position - transform.position;                         //calcola la direzione ad ogni checkpoint
        mover.SetInputNormalized(direction);                                                            //lo passa a TopDownMover2D e normalizza
    }
}