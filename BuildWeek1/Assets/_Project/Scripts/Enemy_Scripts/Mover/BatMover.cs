using UnityEngine;

public class BatMover : MonoBehaviour
{
    private TopDownMover2D mover;
    [SerializeField] private PlayerController player;
    [SerializeField] private int batDmg = 1;

    private Rigidbody2D rb;
    private EnemyDrop drop;
    private Transform playerTransform;
    private LifeController life;
    private EnemiesAnimationHandler _enemyController;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mover = GetComponent<TopDownMover2D>();
        drop = GetComponent<EnemyDrop>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
        life = GetComponent<LifeController>();
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

    private void Update()
    {
        if (life != null && life.IsAlive())
        {
            EnemyMovement();
        }
    }

    private void EnemyMovement()        //sistema di movimento per cui il bat seguirà il player
    {
        Vector2 direction = (playerTransform.position - transform.position);
        mover.SetInputNormalized(direction);
        _enemyController.MovementAnimation(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision) //oncollision fa batDmg, prova a droppare e si distrugge. nota: invertire droppare e distrugge pu� causare problemi?
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LifeController playerLife = collision.gameObject.GetComponent<LifeController>();

            if (playerLife != null)
            {
                playerLife.TakeDamage(batDmg);
            }

            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (!life.IsAlive())
            {
                Die();
            }
            else
            {
                _enemyController.PlayDamageAnimation();
            }
        }
    }

    private void Die()
    {
        if (life.IsAlive()) return;

        if (mover != null) mover.enabled = false;

        if (rb != null) rb.velocity = Vector2.zero;

        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        if (collider != null) collider.enabled = false;

        if (_enemyController != null) _enemyController.DeathAnimation();

        Debug.Log("prova a droppare");
        if (drop != null) drop.TryDrop();
    }
}
