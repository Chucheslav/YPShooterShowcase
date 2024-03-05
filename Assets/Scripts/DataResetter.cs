using UnityEngine;

public class DataResetter : MonoBehaviour
{
    public HealthSo playerHealth;

    private void Awake()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
    }
}
