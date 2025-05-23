using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Elemenets")]
    [SerializeField] private WeaponPosition[] weaponPositions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public bool TryAddWeapon(WeaponDataSO weapon,int level)
    {
        for (int i = 0; i < weaponPositions.Length; i++)
        {
            if (weaponPositions[i].Weapon != null)
                continue;

            weaponPositions[i].AssignWeapon(weapon.Prefab, level);
            return true;
        }

        return false;
    }
}
