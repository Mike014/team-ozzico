using UnityEngine;

public class PhantomMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float timer = 2f;
    private EnemyDrop drop;
    private EnemyLifeController life;
    Vector3 dir;

    private void Awake()
    {
        life = GetComponent<EnemyLifeController>();
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
            //collision.gameObject.GetComponent<Bullet>() -> si legge il danno del bullet e si passa il takedamage
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
        life.IsAlive();
    }
}
