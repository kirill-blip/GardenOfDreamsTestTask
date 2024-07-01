using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    [Space(10f)]
    [SerializeField] private InventoryItem _currentBodyArmor;
    [SerializeField] private Image _bodyArmorImage;
    [SerializeField] private TextMeshProUGUI _bodyArmorText;

    private int _bodyArmorShield;

    public void EquipBodyArmor(Jacket jacket = null, BodyArmor bodyArmor = null)
    {
        if (jacket is not null)
        {
            _currentBodyArmor = jacket;
            _bodyArmorImage.sprite = jacket.Sprite;
            _bodyArmorText.text = jacket.Shield.ToString();
            _bodyArmorShield = jacket.Shield;
        }

        if (bodyArmor is not null)
        {
            _currentBodyArmor = bodyArmor;
            _bodyArmorImage.sprite = bodyArmor.Sprite;
            _bodyArmorText.text = bodyArmor.Shield.ToString();
            _bodyArmorShield = bodyArmor.Shield;
        }
    }

    public int GetBodyShield()
    {
        if (_currentBodyArmor is not null)
        {
            return _bodyArmorShield;
        }

        return 0;
    }

    public int GetHeadShield()
    {
        return 0;
    }

    public bool HasBodyArmor()
    {
        return _currentBodyArmor is not null;
    }

    public InventoryItem GetInventoryItem()
    {
        return _currentBodyArmor;
    }
}
