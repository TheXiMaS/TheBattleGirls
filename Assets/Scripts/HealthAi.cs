using UnityEngine;

public class HealthAi : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealthPoints = 100;
    private int _currentHealthPoints;

    [Header("Armor")]
    [SerializeField] private bool armorIsEquipped = false;
    [SerializeField] private int defenceCount;

    [Header("Regeneration")]
    [SerializeField] private bool regeneration = false;
    [SerializeField] private int healingIntencity = 2;
    [SerializeField] private long _timeBetweenRegenerate;

    [Header("Burning")]
    [SerializeField] private int burningDamage = 2;
    private bool _burning = false;
    private long _timeBetweenBurnDamage;


    private void Awake()
    {
        _currentHealthPoints = maxHealthPoints;
    }

    private void Update()
    {
        if (_currentHealthPoints == 0) SetDeath();
    }

    public void SetDeath()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{gameObject.name} HP: {_currentHealthPoints} - {damage}!");
        if (damage > _currentHealthPoints)
        {
            _currentHealthPoints = 0;
        }
        else
        {
            _currentHealthPoints -= damage;
        }
    }

    public double GetCurrentHP() => _currentHealthPoints;
}
