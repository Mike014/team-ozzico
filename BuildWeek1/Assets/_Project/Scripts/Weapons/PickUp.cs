using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab; // pu� essere lo stesso GameObject se vuoi
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerWeaponList collector = collision.GetComponentInParent<PlayerWeaponList>();
        if (collector != null)
        {
            Debug.Log("Player ha raccolto l'arma!");
            // Raccoglie l'arma
            collector.CollectWeapon(weaponPrefab);
            Destroy(gameObject); // rimuove pickup dalla scena
        }
    }
}
