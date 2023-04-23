using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBar;

    private int _healthValue;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.text = _healthValue.ToString();
        healthBar.value = _healthValue;

        gameObject.SetActive(_healthValue != 0);
    }

    public void SetValue(int healthAmount)
    {
        _healthValue = healthAmount;
        UpdateUI();
    }
}
