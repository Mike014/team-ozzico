using UnityEngine;

public class BeybladeMover : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject[] checkpoints;
    [SerializeField] private float damageInterval = 0.5f; //tempo tra un hit e l'altro durante la collisione
    //[SerializeField] private float distanceSensibility = 0.1f;
    private TopDownMover2D mover;
    private EnemyDrop drop;
    private LifeController life;
    private Vector3 direction;
    private int index;
    private float damageTimer = 0f;
    
    //collider ONTRIGGER!!!!

    private void Awake()
    {
        drop = GetComponent<EnemyDrop>();
        life = GetComponent<LifeController>();
        mover = GetComponent<TopDownMover2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            int damage = bullet.GetDamage();
            life.TakeDamage(damage);

            if (!life.IsAlive())
            {
                if (drop != null)
                {
                    drop.TryDrop();
                }

                //animazione di morte
                //inserisci qui animazione

                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            LifeController playerLife = collision.gameObject.GetComponent<LifeController>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);

                //animazione di contatto
                //inserisci qui animazione
            }

            //reset timer per danno continuo
            damageTimer = damageInterval;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0f)
            {
                LifeController playerLife = collision.gameObject.GetComponent<LifeController>();
                if (playerLife != null)
                {
                    playerLife.TakeDamage(damage);
                }

                //reset timer
                damageTimer = damageInterval;
            }
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, checkpoints[index].transform.position);   //calcola la distanza tra la posizione dell'enemy e quella del checkpoint

        if (distance <= 0.1f)
        {
            index++;                                     //passa al prossimo checkpoint
            if (index >= checkpoints.Length) index = 0;  //quando raggiunge l'ultimo waypoint resetta, così da garantire un loop di movimento
        }

        direction = checkpoints[index].transform.position - transform.position;                         //calcola la direzione ad ogni checkpoint
        mover.SetInputNormalized(direction);                                                            //lo passa a TopDownMover2D e normalizza
    }
}