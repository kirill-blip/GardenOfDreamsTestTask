using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private InventoryItem _currentBodyArmor;
    [SerializeField] private Image _bodyArmorImage;
    [SerializeField] private TextMeshProUGUI _bodyArmorText;

    [Space(10f)]
    [SerializeField] private InventoryItem _currentHeadArmor;
    [SerializeField] private Image _headArmorImage;
    [SerializeField] private TextMeshProUGUI _headArmorText;

    private int _bodyArmorShield;
    private int _headArmorShield;

    public void EquipBodyArmor(ShieldItem shield)
    {
        _currentBodyArmor = shield;
        _bodyArmorImage.sprite = shield.Sprite;

        _bodyArmorText.text = shield.Shield.ToString();
        _bodyArmorShield = shield.Shield;
    }

    public void EquipHeadArmor(Cap cap = null, Halmet halmet = null)
    {
        if (cap is not null)
        {
            _currentHeadArmor = cap;
            _headArmorImage.sprite = cap.Sprite;
            _headArmorText.text = cap.Shield.ToString();
            _headArmorShield = cap.Shield;
        }

        if (halmet is not null)
        {
            _currentHeadArmor = halmet;
            _headArmorImage.sprite = halmet.Sprite;
            _headArmorText.text = halmet.Shield.ToString();
            _headArmorShield = halmet.Shield;
        }
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
        if (equipment.BodyIventoryItem is ShieldItem shieldItem)
        {
            EquipBodyArmor(shieldItem);
        }

        if (equipment.HeadIventoryItem is not null)
        {
            switch (equipment.HeadIventoryItem)
            {
                case Halmet halmet:
                    EquipHeadArmor(halmet: halmet);
                    break;
                case Cap cap:
                    EquipHeadArmor(cap: cap);
                    break;
            }
        }
    }
}
