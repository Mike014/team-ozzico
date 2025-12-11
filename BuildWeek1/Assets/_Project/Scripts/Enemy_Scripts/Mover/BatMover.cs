using UnityEngine;

public class BatMover : MonoBehaviour
{
    [SerializeField] private TopDownMover2D mover;
    [SerializeField] private PlayerController player;
    [SerializeField] private int batDmg = 1;

    private EnemyDrop drop;
    private Transform playerTransform;


    private void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
        drop = GetComponent<EnemyDrop>();
    }

    private void Start()
    {
        if (player == null)         // associa il target verso cui il Bat si dirigerà
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerController>();

                if (player == null)
                {
                    Debug.LogWarning("Lo script PlayerController non è attaccato a questo oggetto!");
                }
            }
            else
            {
                Debug.LogWarning("Player non trovato nella scena!");
            }
        }

        if (player != null) 
        {
            playerTransform = player.transform;
        }
    }

    private void EnemyMovement()        //sistema di movimento per cui il bat seguirà il player
    {
        if (player != null)
        {
            Vector2 direction = (playerTransform.position - transform.position);
            mover.SetInputNormalized(direction);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)      //oncollision fa batDmg, prova a droppare e si distrugge. nota: invertire droppare e distrugge può causare problemi?
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LifeController life = collision.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(batDmg);
            }

            if (drop != null)
            {
                drop.TryDrop();
            }

            //<------                 animazione di esplosione del bat
            Destroy(gameObject);    //non so se serve delay
        }
    }

    private void Update()
    {
        EnemyMovement();
    }
}
