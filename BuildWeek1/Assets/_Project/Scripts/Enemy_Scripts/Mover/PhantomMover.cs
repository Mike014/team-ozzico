using UnityEngine;

public class PhantomMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float timer = 2f;
    private EnemyDrop drop;
    private LifeController life;
    Vector3 dir;

    private EnemiesAnimationHandler _enemyController;

    private void Awake()
    {
        life = GetComponent<LifeController>();
        drop = GetComponent<EnemyDrop>();
        _enemyController = GetComponentInChildren<EnemiesAnimationHandler>();
    }

    private void PhantomMovement()                                                  //mover del ghost
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)                                                            //setto il timer ogni 2 secondi
        {
            float h = Random.Range(-1f, 1f);
            float v = Random.Range(-1f, 1f);
            float z = 0;
            dir = new Vector3(h, v, z).normalized;                                  //calcola dir con numeri random
            timer = 2f;
        }
        transform.position += dir * speed * Time.deltaTime;                         //muove il phantom
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // --> aggiungere collider con PLAYER
        {
            if (!life.IsAlive())
            {
                _enemyController.DeathAnimation();
                if (drop != null)
                {
                    drop.TryDrop();
                }
            }
            else
            {
                _enemyController.PlayDamageAnimation();
            }
        }
    }

    private void Update()
    {
        PhantomMovement();
    }
}