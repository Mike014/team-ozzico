using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager; // Riferimento all'inventario del pg
    [SerializeField] private Transform bulletSpawnPoint;

    private void Start()
    {
        if (weaponManager == null) return;
        // Primo colpo immediato di tutte le armi
        foreach (Weapon weapon in weaponManager.GetAllWeapons())
        {
            if (weapon == null) continue;

            Vector2[] directions = GetWeaponDirections(weapon);

            weapon.ShootAllDirections(directions, transform.position, true); // ignora fireRate per primo colpo

        }
    }

    private void Update()
    {
        if (weaponManager == null) return;

        weaponManager.RemoveDestroyedWeapons();

        // Chiamata a Shoot per tutte le armi ogni frame, lasciando che la singola arma gestisca il fireRate
        foreach (Weapon weapon in weaponManager.GetAllWeapons())
        {
            if (weapon == null) continue;

            Vector2[] directions = GetWeaponDirections(weapon);
            
            weapon.ShootAllDirections(directions, bulletSpawnPoint.position); // fireRate rispettato internamente
        }
    }

    private Vector2[] GetWeaponDirections(Weapon weapon) //Gestisce gli spari in base alle armi (le ho messe per nome ma volendo possiamo modificare)
    {
        if (weapon == null) return new Vector2[] { Vector2.right };

        string name = weapon.GetWeaponName().ToLower();

        if (name.Contains("spaccatesta")) return new Vector2[] { Vector2.right, Vector2.left };

        if (name.Contains("veronica")) return new Vector2[] { Vector2.up, Vector2.down };

        if (name.Contains("lasersonico")) return new Vector2[] { Vector2.up }; // direzione iniziale per homing

        return new Vector2[] { Vector2.right };
    }
}
