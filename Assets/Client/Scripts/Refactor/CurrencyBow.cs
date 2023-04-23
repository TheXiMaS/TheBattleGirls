using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyBow : MonoBehaviour
{
    [SerializeField] protected string currencyName = "Bow";
    [SerializeField] [Min(0)] protected int startCount;
   
    [SerializeField] protected Image imageUI;
    [SerializeField] protected TextMeshProUGUI textUI;

    protected int currentCount;

    private void Start()
    {
        currentCount = startCount;
    }

    private void Update()
    {
        if (textUI.text != currentCount.ToString())
        {
            textUI.text = currentCount.ToString();
        }
    }

    public void AddCurrency(int count)
    {
        currentCount += count;
    }

    public void LossCurrency(int count)
    {
        if (count < currentCount)
        {
            currentCount -= count;
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }

    protected int Count => currentCount;
}
