using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    public WeaponData GetWeaponType()
    {
        return _weaponData;
    }

    public int GetDamage()
    {
        return _weaponData.Damage;
    }

    public int GetCountOfShots()
    {
        return _weaponData.CountOfShots;
    }
}
