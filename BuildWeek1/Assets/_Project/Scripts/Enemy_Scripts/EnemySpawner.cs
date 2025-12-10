using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 2f;        // Ogni quanto spawnerebbe
    public float spawnDistance = 8f;        // Distanza fissa dal player
    public float randomSpread = 2f;         // Randomizzazione aggiuntiva

    private Transform player;
    private float timer;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;
        else
            Debug.LogError("EnemySpawner: Player con tag 'Player' non trovato!");
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SimulateSpawn();
            timer = spawnInterval;
        }
    }

    void SimulateSpawn()
    {
        // Direzione random su cerchio
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        // Distanza = fissa + piccola variazione
        float finalDistance = spawnDistance + Random.Range(-randomSpread, randomSpread);

        Vector2 spawnPos = (Vector2)player.position + randomDir * finalDistance;

        // 🔵 DEBUG: mostra dove spawnerebbe il nemico
        float dist = Vector2.Distance(player.position, spawnPos);

        Debug.Log($"[Test Spawn] Posizione: {spawnPos} | Distanza dal Player: {dist}");
    }
}
