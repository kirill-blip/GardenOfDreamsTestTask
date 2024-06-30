using UnityEngine;
using UnityEngine.UI;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _hitPoint = 50;

    [Space(10f)]
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _deleteButton;


    private void Start()
    {
        _healButton.onClick.AddListener(Heal);
        _deleteButton.onClick.AddListener(Delete);
    }

    private void Heal()
    {
        FindObjectOfType<Player>().Heal(_hitPoint);
    }

    private void Delete()
    {
        FindObjectOfType<InventoryManager>().RemoveItem();
    }
}
