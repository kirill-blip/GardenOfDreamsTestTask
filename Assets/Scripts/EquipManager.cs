using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private Image _bodyArmorImage;
    [SerializeField] private TextMeshProUGUI _bodyArmorText;
    
    [Header("Head")]
    [SerializeField] private Image _headArmorImage;
    [SerializeField] private TextMeshProUGUI _headArmorText;
    
    private InventoryItem _currentBodyArmor;
    private InventoryItem _currentHeadArmor;

    private int _bodyArmorShield;
    private int _headArmorShield;

    public void EquipBodyArmor(ShieldItem shield)
    {
        _currentBodyArmor = shield;
        _bodyArmorImage.sprite = shield.Sprite;

        _bodyArmorText.text = shield.Shield.Value.ToString();
        _bodyArmorShield = shield.Shield.Value;
    }

    public void EquipHeadArmor(ShieldItem shieldItem)
    {
        _currentHeadArmor = shieldItem;
        _headArmorImage.sprite = shieldItem.Sprite;

        _headArmorText.text = shieldItem.Shield.Value.ToString();
        _headArmorShield = shieldItem.Shield.Value;
    }

    public int GetBodyShield()
    {
        return _bodyArmorShield;
    }

    public int GetHeadShield()
    {
        return _headArmorShield;
    }

    public bool HasBodyArmor()
    {
        return _currentBodyArmor is not null;
    }

    public bool HasHeadArmor()
    {
        return _currentHeadArmor is not null;
    }

    public InventoryItem GetBodyArmor()
    {
        return _currentBodyArmor;
    }

    public InventoryItem GetHeadArmor()
    {
        return _currentHeadArmor;
    }

    public void Equip(EquipmentData equipment)
    {
        if (equipment.BodyIventoryItem is BodyShield shieldItem)
        {
            EquipBodyArmor(shieldItem);
        }

        if (equipment.HeadIventoryItem is HeadShield headShield)
        {
            EquipHeadArmor(headShield);
        }
    }
}
