using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 100;
    private int currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    private void Update()
    {
        if (currentHealthPoints == 0) SetDeath();
    }

    public void SetDeath()
    {
        Destroy(gameObject);
    }

    public void GetDamage(int damage)
    {
        Debug.Log($"{gameObject.name} HP: {currentHealthPoints} - {damage}!");
        if (damage > currentHealthPoints)
        {
            currentHealthPoints = 0;
        }
        else
        {
            currentHealthPoints -= damage;
        }
    }

    public int getCurrentHP() => currentHealthPoints;
}
