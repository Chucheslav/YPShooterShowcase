using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private HealthSo health;
    [SerializeField] private SimpleEvent playerDeathEvent;
    

    public void TakeDamage(float value)
    {
        health.currentHealth -= value;
        if (health.currentHealth <= 0)
        {
            playerDeathEvent.Raise();
        }
    }
}
