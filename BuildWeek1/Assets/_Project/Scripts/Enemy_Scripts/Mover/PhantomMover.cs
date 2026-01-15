using UnityEngine;

public class PhantomMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private EnemyDrop drop;
    private LifeController life;
    private EnemiesAnimationHandler _enemyController;
    private Rigidbody2D rb;

    private Vector3 dir;
    private float timer = 2f;

    private void Awake()
    {
        life = GetComponent<LifeController>();
        drop = GetComponent<EnemyDrop>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
        rb = GetComponent<Rigidbody2D>();

        //direzione iniziale casuale, per evitare stia fermo "timer" secondi all'inizio
        float h = Random.Range(-1f, 1f);
        float v = Random.Range(-1f, 1f);
        dir = new Vector3(h, v, 0).normalized;
    }

    private void Update()
    {
        if (life != null && life.IsAlive())
        {
            PhantomMovement();
        }
    }

    private void PhantomMovement()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            float h = Random.Range(-1f, 1f);
            float v = Random.Range(-1f, 1f);
            dir = new Vector3(h, v, 0).normalized;
            timer = 2f;
        }

        transform.position += dir * speed * Time.deltaTime;
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

        if (rb != null) rb.velocity = Vector2.zero;

        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        if (collider != null) collider.enabled = false;

        if (_enemyController != null) _enemyController.DeathAnimation();

        if (drop != null) drop.TryDrop();
    }
}
