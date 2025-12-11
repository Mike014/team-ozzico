using UnityEngine;

public class BatMover : MonoBehaviour
{
    [SerializeField] private TopDownMover2D mover;
    [SerializeField] private PlayerController player;
    [SerializeField] private int batDmg = 1;

    private LifeController life;
    private EnemyDrop drop;
    private Transform playerTransform;
    private EnemiesAnimationHandler _enemyController;

    private void Awake()
    {
        life = GetComponent<LifeController>();
        mover = GetComponent<TopDownMover2D>();
        drop = GetComponent<EnemyDrop>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
    }

    private void Start()
    {
        if (player == null) //associa il target verso cui il Bat si dirigerà
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerController>();
                if (player == null)
                    Debug.LogWarning("PlayerController non attaccato a questo oggetto");
            }
            else
            {
                Debug.LogWarning("Player non trovato nella scena");
            }
        }

        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void EnemyMovement() //il bat segue il player
    {
        if (player != null)
        {
            Vector2 direction = (playerTransform.position - transform.position);
            mover.SetInputNormalized(direction);
            _enemyController.MovementAnimation(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            mover.SetInputNormalized(Vector2.zero);            //ferma il movimento del bat

            //animazione di esplosione
            //inserisci qui animazione

            //chiamata all'evento dell'animazione per infliggere danno
            ExplosionDamage();

            if (drop != null)
                drop.TryDrop();

            //distruggi bat dopo la durata dell’animazione, messo 1f per ora
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //gestione danni da bullet
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                int damage = bullet.GetDamage();
                life.TakeDamage(damage);

                if (!life.IsAlive())
                {
                    //animazione di esplosione
                    //inserisci qui animazione

                    if (drop != null)
                        drop.TryDrop();

                    //distruggi bat dopo la durata dell’animazione, messo 1f per ora
                    Destroy(gameObject, 1f);
                }
            }
        }
    }

    private void Update()
    {
        EnemyMovement();
    }

    //funzione chiamata come event al momento dell'esplosione
    public void ExplosionDamage()
    {
        if (player != null)
        {
            LifeController playerLife = player.GetComponent<LifeController>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(batDmg);
            }
        }
    }
}