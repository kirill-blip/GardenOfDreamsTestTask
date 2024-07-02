using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverPanel _gameOverPanel;

    private Player _player;
    private Enemy _enemy;
    private InventoryManager _inventoryManager;
    private SaveSystem _saveSystem;

    private void Awake()
    {
        _saveSystem = FindObjectOfType<SaveSystem>();
        _saveSystem.Load();
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemy = FindObjectOfType<Enemy>();
        _inventoryManager = FindObjectOfType<InventoryManager>();

        _player.Health.EntityDied += PlayerDiedHandler;
        _enemy.Health.EntityDied += EnemyDiedHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnApplicationQuit()
    {
        _saveSystem.Save();
    }

    private void PlayerDiedHandler()
    {
        _gameOverPanel.gameObject.SetActive(true);
        _saveSystem.DeleteData();
    }

    private void EnemyDiedHandler()
    {
        _inventoryManager.AddRandomItem();
        _enemy.Health.ResetHealth();
    }
}
