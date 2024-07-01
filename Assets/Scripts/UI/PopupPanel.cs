using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    [SerializeField] private GameObject _shieldInfo;
    [SerializeField] private TextMeshProUGUI _shieldInfoText;
    [SerializeField] private TextMeshProUGUI _hitPointText;

    [Space(10f)]
    [SerializeField] private Image _image;

    [Space(10f)]
    [SerializeField] private Button _deleteButton;

    [Space(10f)]
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;

    private InventoryManager _inventoryManager;
    private InventoryItem _inventoryItem;

    private void Start()
    {
        _inventoryManager = FindObjectOfType<InventoryManager>();

        _deleteButton.onClick.AddListener(DeleteButtonHandler);

        _healButton.onClick.AddListener(HealButtonHandler);
        _buyButton.onClick.AddListener(BuyButtonHandler);
        _equipButton.onClick.AddListener(EquipButtonHandler);
    }

    private void DeleteButtonHandler()
    {
        _inventoryManager.RemoveItem(_inventoryItem);
        Disenable();
    }

    private void HealButtonHandler()
    {
        _inventoryManager.RemoveItem(_inventoryItem);
        FindObjectOfType<Player>().Health.Heal((_inventoryItem as AidKit).HitPoints);

        Disenable();
    }

    private void BuyButtonHandler()
    {
        _inventoryManager.FillBullets(_inventoryItem);
        Disenable();
    }

    private void EquipButtonHandler()
    {
        EquipManager equipManager = FindObjectOfType<EquipManager>();

        if (_inventoryItem.GetType() == typeof(BodyArmor))
        {
            if (!equipManager.HasBodyArmor())
            {
                _inventoryManager.RemoveItem(_inventoryItem);
                equipManager.EquipBodyArmor(bodyArmor: (_inventoryItem as BodyArmor));
            }
            else
            {
                _inventoryManager.Swap(_inventoryItem, equipManager.GetInventoryItem());
                equipManager.EquipBodyArmor(bodyArmor: (_inventoryItem as BodyArmor));
            }
        }
        else if (_inventoryItem.GetType() == typeof(Jacket))
        {
            if (!equipManager.HasBodyArmor())
            {
                _inventoryManager.RemoveItem(_inventoryItem);
                equipManager.EquipBodyArmor(jacket: (_inventoryItem as Jacket));
            }
            else
            {
                _inventoryManager.Swap(_inventoryItem, equipManager.GetInventoryItem());
                equipManager.EquipBodyArmor(jacket: (_inventoryItem as Jacket));
            }
        }

        Disenable();
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    private void Disenable()
    {
        gameObject.SetActive(false);
    }

    public void ActivatePanel(InventoryItem inventoryItem)
    {
        Enable();

        _inventoryItem = inventoryItem;
        _image.sprite = _inventoryItem.Sprite;

        ResetPanel();

        switch (inventoryItem)
        {
            case AidKit aidKit:
                _healButton.gameObject.SetActive(true);
                _hitPointText.gameObject.SetActive(true);

                _hitPointText.text = $"+{aidKit.HitPoints}HP";
                break;
            case Bullets:
                _buyButton.gameObject.SetActive(true);
                break;
            case BodyArmor bodyArmor:
                _equipButton.gameObject.SetActive(true);
                _shieldInfo.gameObject.SetActive(true);

                _shieldInfoText.text = bodyArmor.Shield.ToString();
                break;
            case Jacket jacket:
                _equipButton.gameObject.SetActive(true);
                _shieldInfo.gameObject.SetActive(true);

                _shieldInfoText.text = jacket.Shield.ToString();
                break;
        }
    }

    public void ResetPanel()
    {
        _shieldInfo.SetActive(false);
        _hitPointText.gameObject.SetActive(false);

        _healButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);
    }
}
