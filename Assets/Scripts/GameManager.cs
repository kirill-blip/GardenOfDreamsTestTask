﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _headDamageToPlayer = 15;
    [SerializeField] private int _damageToPlayer = 10;

    [Space(10f)]
    [SerializeField] private Button _pistolButton;
    [SerializeField] private Button _rifleButton;
    [SerializeField] private Button _shootButton;

    [Space(10f)]
    [SerializeField] private Weapon _rifleWeapon;
    [SerializeField] private Weapon _pistolWeapon;

    [Space(10f)]
    [SerializeField] private GameOverPanel _gameOverPanel;

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

        _player.Health.EntityDied += PlayerDiedHandler;
        _enemy.Health.EntityDied += EnemyDiedHandler;

        _player.SetWeapon(_pistolWeapon);

        _pistolButton.onClick.AddListener(() => _player.SetWeapon(_pistolWeapon));
        _rifleButton.onClick.AddListener(() => _player.SetWeapon(_rifleWeapon));

        _shootButton.onClick.AddListener(() => Shoot());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void PlayerDiedHandler()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }

    private void EnemyDiedHandler()
    {
        _inventoryManager.AddRandomItem();
        _enemy.Health.ResetHealth();
    }

    private void Shoot()
    {
        if (_inventoryManager.HasBullets(_player.CurrentWeapon.GetWeaponType()))
        {
            for (int i = 0; i < _player.CurrentWeapon.GetCountOfShots(); i++)
            {
                _enemy.Health.Damage(_player.CurrentWeapon.GetDamage());
            }

            //if (_isPreviousHead)
            //{
            //    int actualDamage = _headDamageToPlayer - _equipManager.GetHeadShield();
            //    _player.Health.Damage(actualDamage);
            //    _isPreviousHead = !_isPreviousHead;
            //}
            //else
            {
                int actualDamage = _damageToPlayer - _equipManager.GetBodyShield();
                _player.Health.Damage(actualDamage);
                _isPreviousHead = !_isPreviousHead;
            }

            _inventoryManager.RemoveBullet(_player.CurrentWeapon.GetWeaponType(), _player.CurrentWeapon.GetCountOfShots());
        }
    }
}
