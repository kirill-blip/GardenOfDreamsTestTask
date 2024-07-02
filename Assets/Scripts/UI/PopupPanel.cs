using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private GameObject _shieldInfo;
    [SerializeField] private TextMeshProUGUI _shieldInfoText;
    [SerializeField] private TextMeshProUGUI _hitPointText;

    [Space(10f)]
    [SerializeField] private int _defaultPositionXOfWeight = 100;
    [SerializeField] private RectTransform _weightInfo;
    [SerializeField] private TextMeshProUGUI _weightText;

    [Space(10f)]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    [Space(10f)]
    [SerializeField] private Button _deleteButton;
    [SerializeField] private int _defaultPositionXOfDeleteButton = 125;

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
        FindObjectOfType<Player>().Health.Heal((_inventoryItem as AidKit).HitPoints.Value);

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

        if (_inventoryItem is BodyShield bodyShield)
        {
            if (equipManager.HasBodyArmor())
            {
                _inventoryManager.Swap(_inventoryItem, equipManager.GetBodyArmor());
            }
            else
            {
                _inventoryManager.RemoveItem(_inventoryItem);
            }

            equipManager.EquipBodyArmor(bodyShield);
        }

        if (_inventoryItem is HeadShield headShield)
        {
            if (equipManager.HasHeadArmor())
            {
                _inventoryManager.Swap(_inventoryItem, equipManager.GetHeadArmor());
            }
            else
            {
                _inventoryManager.RemoveItem(_inventoryItem);
            }

            equipManager.EquipHeadArmor(headShield);
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
        _text.text = inventoryItem.Name;
        _weightText.text = inventoryItem.Weight.ToString();

        ResetPanel();

        switch (inventoryItem)
        {
            case AidKit aidKit:
                _healButton.gameObject.SetActive(true);
                _hitPointText.gameObject.SetActive(true);

                _hitPointText.text = $"+{aidKit.HitPoints.Value}HP";
                break;
            case Bullets:
                _buyButton.gameObject.SetActive(true);
                _weightInfo.anchoredPosition = new Vector2(0, _weightInfo.anchoredPosition.y);
                break;
            case ShieldItem shieldItem:
                _equipButton.gameObject.SetActive(true);
                _shieldInfo.gameObject.SetActive(true);

                _shieldInfoText.text = shieldItem.Shield.Value.ToString();
                break;
            default:
                RectTransform buttonTransform = _deleteButton.GetComponent<RectTransform>();
                buttonTransform.anchoredPosition = new Vector2(0, buttonTransform.anchoredPosition.y);
                break;
        }
    }

    public void ResetPanel()
    {
        RectTransform buttonTransform = _deleteButton.GetComponent<RectTransform>();
        buttonTransform.anchoredPosition = new Vector2(_defaultPositionXOfDeleteButton, buttonTransform.anchoredPosition.y);

        _weightInfo.anchoredPosition = new Vector2(_defaultPositionXOfWeight, _weightInfo.anchoredPosition.y);

        _shieldInfo.SetActive(false);
        _hitPointText.gameObject.SetActive(false);

        _healButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);
    }
}
