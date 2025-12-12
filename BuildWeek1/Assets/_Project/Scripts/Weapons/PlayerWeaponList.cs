using UnityEngine;

public class PlayerWeaponList : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    public void CollectWeapon(Weapon weaponPrefab) // Chiamato quando il player raccoglie un'arma
    {
        if (weaponPrefab == null) return;

        foreach (Weapon w in weaponManager.GetAllWeapons()) // Controlla se l'arma è già posseduta
        {
            if (w != null && w.GetWeaponName() == weaponPrefab.GetWeaponName())
            {
                w.LevelUp(); // Potenzia l'arma già posseduta
                Debug.Log($"Arma {w.GetWeaponName()} potenziata!");
                return;
            }
        }

        Weapon newWeapon = Instantiate(weaponPrefab, transform); // Istanzia arma come figlia del player
        newWeapon.transform.localPosition = Vector3.zero; // posizione in base al player
        weaponManager.AddOrLevelUpWeapon(newWeapon);

        Debug.Log($"Arma {newWeapon.GetWeaponName()} raccolta e aggiunta al WeaponManager");
    }
}
