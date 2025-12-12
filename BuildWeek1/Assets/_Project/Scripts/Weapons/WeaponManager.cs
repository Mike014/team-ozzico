using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons = new List<Weapon>(); // Lista delle armi

    public event Action<List<Weapon>> OnWeaponListChanged;

    public List<Weapon> GetAllWeapons() => weapons; // Da tutte le armi al player

    public void AddOrLevelUpWeapon(Weapon newWeapon) // Aggiunge una nuova arma al player
    {
        if (newWeapon == null) return;

        string newName = newWeapon.GetWeaponName();
        foreach (Weapon w in weapons)
        {
            if (w != null && w.GetWeaponName() == newName)
            {
                w.LevelUp();
                Debug.Log($"WeaponManager: {newName} gia' esistente, l'arma e' salita di livello!");
                OnWeaponListChanged?.Invoke(weapons);
                return;
            }
        }

        weapons.Add(newWeapon);
        Debug.Log($"WeaponManager: Arma aggiunta: {newWeapon.GetWeaponName()}");
        OnWeaponListChanged?.Invoke(weapons);
    }

    public void RemoveDestroyedWeapons()
    {
        weapons.RemoveAll(w => w == null); // rimuove armi distrutte dalla lista
        OnWeaponListChanged?.Invoke(weapons);
    }
}