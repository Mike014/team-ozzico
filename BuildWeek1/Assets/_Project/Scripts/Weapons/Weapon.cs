using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bulletPrefab; // prefab del proiettile

    [SerializeField] private bool isDoubleShot = false; // per destra e sinistra 2 colpi
    [SerializeField] private bool isHoming = false; // per arma che cerca il nemico

    private float lastFireTime; // Timestamp dell'ultimo colpo sparato, per rispettare il fireRate

    private void Awake()
    {
        lastFireTime = -1f / fireRate; // Forza il primo colpo immediato
    }

    public string GetWeaponName() => weaponName;

    // Metodo per sparare in piu' direzioni contemporaneamente
    public void ShootAllDirections(Vector2[] directions, Vector3 spawnPosition, bool ignoreFireRate = false)
    {
        // Controlla il fire rate: se non e' ancora passato il tempo necessario e non si ignora, esce
        if (!ignoreFireRate && Time.time < lastFireTime + 1f / fireRate) return;
        lastFireTime = Time.time; // Aggiorna il timestamp dell'ultimo colpo

        foreach (Vector2 dir in directions) // Cicla tutte le direzioni in cui sparare
        {
            if (bulletPrefab == null) continue; // Se non c'e' prefab del proiettile, salta

            if (isHoming)
            {
                HomingShoot(spawnPosition); // Chiamata al metodo che gestisce i proiettili homing
                continue; // Passa al prossimo ciclo
            }

            if (isDoubleShot)
            {
                DoubleShoot(dir, spawnPosition); // Chiamata al metodo che spara due proiettili per direz
                continue;
            }

            GameObject bulletObj = Instantiate(bulletPrefab, spawnPosition, transform.rotation); // Istanzia il proiettile
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
                bullet.SetUp(dir, damage); // Imposta direzione e danno
        }
    }

    private void DoubleShoot(Vector2 dir, Vector3 spawnPosition)
    {
        // Calcola un piccolo offset per separare i due proiettili lateralmente
        Vector2 offset = Vector2.Perpendicular(dir).normalized * 0.18f;

        // Istanzia i due proiettili
        GameObject b1 = Instantiate(bulletPrefab, spawnPosition + (Vector3)offset, transform.rotation);
        GameObject b2 = Instantiate(bulletPrefab, spawnPosition - (Vector3)offset, transform.rotation);

        // Imposta direzione e danno per entrambi
        Bullet bu1 = b1.GetComponent<Bullet>(); if (bu1 != null) bu1.SetUp(dir, damage);
        Bullet bu2 = b2.GetComponent<Bullet>(); if (bu2 != null) bu2.SetUp(dir, damage);
    }

    private void HomingShoot(Vector3 spawnPosition) // Metodo per lo sparo che segue il nemico
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Trova tutti i nemici in scena con tag "Enemy"
        if (enemies.Length == 0)
        {
            GameObject b = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
            Bullet bu = b.GetComponent<Bullet>();
            if (bu != null) bu.SetUp(Vector2.up, damage);
            return;
        }

        Transform closest = enemies[0].transform; // Trova il nemico più vicino
        float bestDist = Vector2.SqrMagnitude((Vector2)closest.position - (Vector2)spawnPosition);
        for (int i = 1; i < enemies.Length; i++)
        {
            float d = Vector2.SqrMagnitude((Vector2)enemies[i].transform.position - (Vector2)spawnPosition);
            if (d < bestDist)
            {
                bestDist = d;
                closest = enemies[i].transform;
            }
        }

        // Istanzia il proiettile e imposta il target per homing
        GameObject bulletObj = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetUp(Vector2.zero, damage, closest, true); // Vector2.zero perche' il movimento iniziale sara' gestito dal target
        }
    }

    public void LevelUp() // Metodo per il potenziamento dell'arma
    {
        damage += 1;
        fireRate += 0.1f;
        Debug.Log($"{weaponName} potenziata: danno={damage}, fireRate={fireRate}");
    }
}