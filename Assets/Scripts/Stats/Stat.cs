using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [SerializeField] private Stat m_ImpactedStat;
    [SerializeField] private float m_ModifierValue;

    public Stat ImpactedStat => m_ImpactedStat;
    public float ModifierValue => m_ModifierValue;
}

[CreateAssetMenu(menuName = "ScriptableObjects/Stats/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField] private float m_BaseValue;

    public float BaseValue => m_BaseValue;
}