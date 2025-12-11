using UnityEngine;

public class BatMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerController player;
    [SerializeField] private int batDmg = 1;
    private EnemyDrop drop;
    private Transform playerTransform;


    private void Awake()
    {
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
        if (player == null)
        {
            return;
        }
        else
        {
            Vector2 enemyPos = transform.position;
            Vector2 playerPos = playerTransform.position;

            gameObject.transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
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
