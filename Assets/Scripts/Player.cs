using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health { get; private set; }

    public Weapon CurrentWeapon { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    public void SetWeapon(Weapon weapon)
    {
        CurrentWeapon = weapon;
    }
}
