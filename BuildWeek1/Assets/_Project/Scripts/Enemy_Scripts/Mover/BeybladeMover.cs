using UnityEngine;

public class BeybladeMover : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject[] checkpoints;
    [SerializeField] private float damageInterval = 0.5f;

    private TopDownMover2D mover;
    private EnemyDrop drop;
    private LifeController life;
    private EnemiesAnimationHandler _enemyController;
    private Rigidbody2D rb;

    private Vector3 direction;
    private int index;
    private float damageTimer = 0f;

    private void Awake()
    {
        drop = GetComponent<EnemyDrop>();
        life = GetComponent<LifeController>();
        mover = GetComponent<TopDownMover2D>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (life != null && life.IsAlive())
        {
            MoveAlongCheckpoints();
        }
    }

    private void MoveAlongCheckpoints()
    {
        if (checkpoints.Length == 0)
            return;

        float distance = Vector2.Distance(transform.position, checkpoints[index].transform.position);
        if (distance <= 0.1f)
        {
            index++;
            if (index >= checkpoints.Length)
                index = 0;
        }

        direction = checkpoints[index].transform.position - transform.position;
        mover.SetInputNormalized(direction);
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

        if (collision.gameObject.CompareTag("Player"))
        {
            LifeController playerLife = collision.gameObject.GetComponent<LifeController>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(damage);
            }

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

                damageTimer = damageInterval;
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

        if (drop != null) drop.TryDrop();
    }
}
