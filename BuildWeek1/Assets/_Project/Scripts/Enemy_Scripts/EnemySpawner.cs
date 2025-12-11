using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;   //array dei prefab nemici
    [SerializeField] private float spawnInterval = 2f;    //ogni quanto spawnare
    [SerializeField] private float spawnDistance = 8f;    //distanza fissa dal player
    [SerializeField] private float randomSpread = 2f;     //piccola variazione casuale

    private Transform player;
    private float timer;

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;
        else
            Debug.LogError("EnemySpawner: Player con tag 'Player' non trovato!");
    }

    private void Update()
    {
        if (player == null || enemyPrefabs.Length == 0) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);       //prende un nemico casuale dall'array di nemici
        GameObject enemyPrefab = enemyPrefabs[index];           //lo assegna

        // Direzione casuale su cerchio
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        // Distanza fissa + variazione
        float finalDistance = spawnDistance + Random.Range(-randomSpread, randomSpread);

        // Posizione finale
        Vector2 spawnPos = (Vector2)player.position + randomDir * finalDistance;

        //instanzia il nemico
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = spawnPos;

        // Debug
        Debug.Log($"{enemyPrefab.name} spawnato in {spawnPos}");
    }
}