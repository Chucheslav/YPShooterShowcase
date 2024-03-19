using UnityEngine;

public class DataResetter : MonoBehaviour
{
    public HealthSo playerHealth;
    public PlayerStats stats;


    private void Awake()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
        stats.Reset();
    }
}
