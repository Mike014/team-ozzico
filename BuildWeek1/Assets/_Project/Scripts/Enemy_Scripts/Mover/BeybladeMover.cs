using UnityEngine;

public class BeybladeMover : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject[] checkpoints;
    private TopDownMover2D mover;
    private EnemyDrop drop;
    private LifeController life;
    private Vector3 direction;
    private int index;

    private void Awake()
    {
        drop = GetComponent<EnemyDrop>();
        life = GetComponent<LifeController>();
        mover = GetComponent<TopDownMover2D>();
    }

    private void Update()
    {
        direction = checkpoints[index].transform.position - transform.position;
        mover.SetInputNormalized(direction);
    }
}
