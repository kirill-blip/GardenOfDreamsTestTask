using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private IntReference _damageToBody;
    [SerializeField] private IntReference _damageToHead;

    [Space(10f)]
    [SerializeField] private Button _pistolButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _rifleButton;

    [Space(10f)]
    [SerializeField] private Weapon _rifleWeapon;
    [SerializeField] private Weapon _pistolWeapon;

    private Player _player;
    private Enemy _enemy;

    private InventoryManager _inventoryManager;
    private EquipManager _equipManager;

    private bool _isPreviousHead = false;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectOfType<Enemy>();
        _inventoryManager = FindObjectOfType<InventoryManager>();
        _equipManager = FindObjectOfType<EquipManager>();

        _player.SetWeapon(_pistolWeapon);

        _pistolButton.onClick.AddListener(() => _player.SetWeapon(_pistolWeapon));
        _rifleButton.onClick.AddListener(() => _player.SetWeapon(_rifleWeapon));

        _shootButton.onClick.AddListener(() => Shoot());
    }

    private void Shoot()
    {
        if (_inventoryManager.HasBullets(_player.CurrentWeapon.GetWeaponType()))
        {
            for (int i = 0; i < _player.CurrentWeapon.GetCountOfShots(); i++)
            {
                _enemy.Health.Damage(_player.CurrentWeapon.GetDamage());
            }

            _player.Health.Damage(CalculateDamage());

            _inventoryManager.RemoveBullet(_player.CurrentWeapon.GetWeaponType(), _player.CurrentWeapon.GetCountOfShots());
        }
    }

    private int CalculateDamage()
    {
        int actualDamage = 0;

        if (_isPreviousHead)
        {
            actualDamage = _damageToBody.Value - _equipManager.GetHeadShield();
        }
        else
        {
            actualDamage = _damageToHead.Value - _equipManager.GetBodyShield();
        }

        _isPreviousHead = !_isPreviousHead;

        return actualDamage;
    }
}
