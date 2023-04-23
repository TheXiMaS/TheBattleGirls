using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [Header("Objects UI")]
    [SerializeField] private GameObject commonUI;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject bottomPanel;

    [Header("Currency UI")] 
    [SerializeField] private GameObject coinsUI;
    [SerializeField] private GameObject bowsUI;
    
    [Header("Weapon UI")]
    [SerializeField] private GameObject weaponUI;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Image fireModeIcon;
    [SerializeField] private TextMeshProUGUI fireModeText;
    
    [Header("Health UI")]
    [SerializeField] private GameObject healthUI;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        topPanel.SetActive(coinsUI.activeSelf || bowsUI.activeSelf);
        bottomPanel.SetActive(weaponUI.activeSelf || healthUI.activeSelf);
        
        commonUI.SetActive(topPanel.activeSelf || bottomPanel.activeSelf);
    }
}
