using UnityEngine;

public class DamageSystem : MonoBehaviour
{

    [SerializeField] private int maxHealthPoints = 200;
    private int _currentHealthPoints;

    private void Awake()
    {
        _currentHealthPoints = maxHealthPoints;
    }

    private void Update()
    {
        if (_currentHealthPoints == 0) DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (damage > _currentHealthPoints)
        {
            _currentHealthPoints = 0;
        }
        else
        {
            _currentHealthPoints -= damage;
        }
    }
}
