using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private HealthSo health;
    [SerializeField] private SimpleEvent playerDeathEvent;
    [field: SerializeField] public PlayerStats stats { get; private set; } 
    

    public void TakeDamage(float value)
    {
        health.currentHealth -= value;
        if (health.currentHealth <= 0)
        {
            playerDeathEvent.Raise();
        }
    }
}
