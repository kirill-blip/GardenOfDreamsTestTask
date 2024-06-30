using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetHealth(int health)
    {
        _text.text = health.ToString();
        _image.fillAmount = health / 100f;
    }
}
