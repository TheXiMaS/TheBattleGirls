using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthAI : MonoBehaviour
{
    [SerializeField] private EffectsUI effectsUI;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] [CustomAttributes.ReadOnly] private float _currentHP;
    [SerializeField] private EffectData[] effectsData;
    [SerializeField] private List<Effect> activeEffects;

    private readonly List<Effect> effectsList;

    private void Start()
    {
        _currentHP = maxHealth;
        InitializeEffects();
    }

    private void InitializeEffects()
    {
        foreach (EffectData data in effectsData)
        {
            effectsList.Add(new Effect(
                data.Id,
                data.Name,
                data.Description,
                data.EffectType,
                data.Duration,
                data.Sprite
                ));

        }


    }
    
    public List<Effect> GetActiveEffects() => activeEffects;

    public void TakeDamage(float amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"{gameObject.name}: damage ({amount}) " +
                $"cannot be less than or equal to 0!");
            return;
        }

        if (amount > _currentHP)
        {
            _currentHP = 0;
        }
        else
        {
            _currentHP -= amount;
            Debug.Log($"{gameObject.name}: HP - {amount} points");
        }

        if (_currentHP == 0) Die();
    }

    public void TakeHealth(float amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"{gameObject.name}: heal ({amount}) " +
                $"cannot be less than or equal to 0!");
            return;
        }

        if (_currentHP == maxHealth)
        {
            Debug.Log($"{gameObject.name}: HP is already full!");
            return;
        }

        if ((_currentHP + amount) > maxHealth)
        {
            _currentHP = maxHealth;
            Debug.Log($"{gameObject.name}: has been healed to Max HP!");
        }
        else
        {
            _currentHP += amount;
            Debug.Log($"{gameObject.name}: HP + {amount} points");
        }
    }

    public void ApplyEffect(int effectID)
    {
        foreach (Effect effect in effectsList)
        {
            if (effect.GetID() == effectID)
            {
                effect.Activate();
                effectsUI.UpdateUI();
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been died.");
        Destroy(gameObject);
    }
}

[System.Serializable]
public class Effect
{
    private int _id;
    private string _name;
    private string _description;
    private EffectType _type;
    private bool _isActive;
    private float _duration;
    private Sprite _sprite;

    public Effect(int id, string name, string description, EffectType type, float duration, Sprite sprite)
    {
        _id = id;
        _name = name;
        _description = description;
        _type = type;
        _isActive = false;
        _duration = duration;
        _sprite = sprite;
    }

    public void Activate()
    {
        _isActive = true;
        Debug.Log($"Effect \"{_name}\" has been activate!");
    }

    public void Deactivate()
    {
        _isActive = false;
        Debug.Log($"Effect \"{_name}\" has been deactivate!");
    }

    public int GetID() => _id;
    public string GetName() => _name;
    public string GetDescription() => _description;
    public EffectType GetEffectType() => _type;
    public Sprite GetSprite() => _sprite;
    public bool IsActive() => _isActive;
}

public enum EffectType
{
    Buff,
    Debuff
}