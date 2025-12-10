using UnityEngine;

public class BatMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerController player;
    private void Start()
    {
        if (player == null)
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
    }

    private void EnemyMovement()
    {
        if (player == null)
        {
            return;
        }
        else
        {
            Vector2 enemyPos = transform.position;
            Vector2 playerPos = player.transform.position;

            gameObject.transform.position = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //animazione di esplosione del bat
            //takedamage al player
            //check vita player
        }
    }

    private void Update()
    {
        EnemyMovement();
    }
}
