using UnityEngine;

public class PhantomMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float timer = 2f;
    private EnemyDrop drop;
    private LifeController life;
    Vector3 dir;

    private void Awake()
    {
        life = GetComponent<LifeController>();
        drop = GetComponent<EnemyDrop>();
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
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        PhantomMovement();
    }
}