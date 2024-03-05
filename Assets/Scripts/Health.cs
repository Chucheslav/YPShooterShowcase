using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxValue = 100;
    [SerializeField] private float currentValue;

    public event Action ZeroHealth; //это событие, его видно снаружи, но вызвать можно только внутри класса
    private Action<float> valueChanged; //это делегат - его видно только внутри потому что он private,
                                        //мы его спрятали потому что подписываться/отписываться будем через методы
    
    /// <summary>
    /// подписываемся на событие изменения значения
    /// </summary>
    /// <param name="method"> сюда передаем сам метод, котрому будет передаваиться пропорция здоровья при изменении</param>
    public void Bind(Action<float> method)
    {
        valueChanged += method;
        method.Invoke(currentValue / maxValue);
    }
    
    /// <summary>
    /// Не забываем вызывать этот метод чтобы отписаться от делегата.
    /// </summary>
    /// <param name="method">сюда передаем тот же метод который передавали в Bind</param>
    public void Unbind(Action<float> method)
    {
        valueChanged -= method;
    }
    
    private void Awake()
    {
        currentValue = maxValue;
    }

    public void DealDamage(float damage)
    {
        currentValue -= damage;
        valueChanged?.Invoke(currentValue/maxValue);
        if (currentValue <= 0) ZeroHealth.Invoke();
    }
}