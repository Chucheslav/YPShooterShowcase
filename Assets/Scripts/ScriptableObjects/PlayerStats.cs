using UnityEngine;

[CreateAssetMenu(menuName = "Group23/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float baseSpeed;
    public HealthSo healthSo;
    [field: SerializeField] public int expPerLevel { get; private set; }
    
    public float moveSpeed;
    [field: SerializeField] public int currentExp { get; private set; }
    public int currentLevel;

    public void AddExp(int amount)
    {
        currentExp = Mathf.Min(expPerLevel, currentExp + amount);
        if (currentExp >= expPerLevel)
        {
            currentExp = 0;
            currentLevel++;
        }
    }

    public void Reset()
    {
        moveSpeed = baseSpeed;
        currentLevel = 1;
        currentExp = 0;
    }
}