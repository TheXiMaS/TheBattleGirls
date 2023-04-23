using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Data", menuName = "Effect Data", order = 52)]
public class EffectData : ScriptableObject
{
    [SerializeField] private EffectType effectType;
    [SerializeField] private int effectId;
    [SerializeField] private string effectName;
    [SerializeField] private string effectDescription;
    [SerializeField] private float effectDuration;
    [SerializeField] private Sprite effectSprite;

    public EffectType EffectType => effectType;
    public int Id => effectId;
    public string Name => effectName;
    public string Description => effectDescription;
    public float Duration => effectDuration;
    public Sprite Sprite => effectSprite;
}
