using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private HealthSo health;

    public void TakeDamage(float value)
    {
        health.currentHealth -= value;
        if (health.currentHealth <= 0)
        {
            Debug.Log("ты дед"); //это временно, скоро починим
        }
    }
}
