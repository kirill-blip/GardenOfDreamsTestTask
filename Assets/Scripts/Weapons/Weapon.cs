using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;

    public WeaponType GetWeaponType()
    {
        return _weaponData.WeaponType;
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
