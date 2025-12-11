using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private int dropChance = 10;

    private void Awake()
    {
        if (weapon == null)
        {
            Debug.LogWarning($"Nessuna arma assegnata al drop di {gameObject.name}");
        }
    }
    private bool HasDropped()
    {
        int random = Random.Range(0, 100);
        if (random < dropChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void TryDrop()       //da checkare se bisogna impostare un delay per la fine dell'animazione di morte
    {
        if (HasDropped())
        {
            Instantiate(weapon, transform.position, Quaternion.identity); 
        }
    }
}