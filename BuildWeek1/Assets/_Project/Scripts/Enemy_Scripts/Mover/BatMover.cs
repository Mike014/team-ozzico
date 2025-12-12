using UnityEngine;

public class BatMover : MonoBehaviour
{
    [SerializeField] private TopDownMover2D mover;
    [SerializeField] private PlayerController player;
    [SerializeField] private int batDmg = 1;

    private EnemyDrop drop;
    private Transform playerTransform;

    private EnemiesAnimationHandler _enemyController;
    private bool isDead;
    private Rigidbody2D _rb;


    private void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
        drop = GetComponent<EnemyDrop>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (player == null)         // associa il target verso cui il Bat si diriger�
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerController>();

                if (player == null)
                {
                    Debug.LogWarning("Lo script PlayerController non � attaccato a questo oggetto!");
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

    private void EnemyMovement()        //sistema di movimento per cui il bat seguir� il player
    {
        if (player != null)
        {
            Vector2 direction = (playerTransform.position - transform.position);
            mover.SetInputNormalized(direction);
            _enemyController.MovementAnimation(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)      //oncollision fa batDmg, prova a droppare e si distrugge. nota: invertire droppare e distrugge pu� causare problemi?
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDead) return;
            isDead = true;

            LifeController life = collision.gameObject.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(batDmg);
            }
            mover.enabled = false;
            _enemyController.DeathAnimation();

            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.transform.position = collision.transform.position;

            if (drop != null)
            {
                drop.TryDrop();
            }

            //<------                 animazione di esplosione del bat
            // Destroy(gameObject);    //non so se serve delay -> Destroy è già dentro l'evento di fine animazione
        }
    }

    private void Update()
    {
        EnemyMovement();
    }
}
