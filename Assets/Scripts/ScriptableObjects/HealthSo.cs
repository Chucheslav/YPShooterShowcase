using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Group23/Health")]
public class HealthSo : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    
    public float GetFraction() => Mathf.Max(currentHealth / maxHealth, 0);
}