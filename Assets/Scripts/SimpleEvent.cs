using System;
using UnityEditor;
using UnityEngine;

//этот скрипт мы еще не обсуждали. Это простое событие, но в скриптовом объекте. На 10м вебе посмотрим подробнее
[CreateAssetMenu(menuName = "Group23/SimpleEvent")] 
public class SimpleEvent : ScriptableObject
{
    [SerializeField] private bool clearOnEnable;
    [SerializeField] private bool clearOnDisable;
    private Action _invoked;

    private void OnEnable()
    {
        if(clearOnEnable) Clear();
    }

    private void OnDisable()
    {
        if(clearOnDisable) Clear();
    }

    
    public void Raise() => _invoked?.Invoke();

    public void Subscribe(Action method) => _invoked += method;
    public void Unsubscribe(Action method) => _invoked -= method;
    public void Clear() => _invoked = null;

    #region Menu Commands

    [MenuItem("CONTEXT/" + nameof(SimpleEvent) + "/Clear Subcribers")]
    static void RaiseEvent(MenuCommand command)
    {
        (command.context as SimpleEvent)?.Clear();
        
    }
    
    [MenuItem("CONTEXT/" + nameof(SimpleEvent) + "/Raise Event")]
    static void ClearSubsribers(MenuCommand command)
    {
        (command.context as SimpleEvent)?.Raise();
    }

    #endregion
    
}