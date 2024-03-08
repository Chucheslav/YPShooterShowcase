using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Group23/TransformVariable")] 
public class TransformVariable : ScriptableObject
{
    [SerializeField] private bool clearOnEnable;
    [SerializeField] private bool clearOnDisable;

    public Transform Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _valueChanged?.Invoke(value);
            
        }
    }
    
    private Action<Transform> _valueChanged;
    private Transform _value;

    private void OnEnable()
    {
        if(clearOnEnable) Clear();
    }

    private void OnDisable()
    {
        if(clearOnDisable) Clear();
    }
    
    public void Raise() => _valueChanged?.Invoke(_value);

    public void Subscribe(Action<Transform> method) => _valueChanged += method;

    public void Bind(Action<Transform> method)
    {
        _valueChanged += method;
        method.Invoke(_value);
    }
    public void Unsubscribe(Action<Transform> method) => _valueChanged -= method;
    public void Clear() => _valueChanged = null;

    #region Menu Commands

    [MenuItem("CONTEXT/" + nameof(TransformVariable) + "/Clear Subcribers")]
    static void RaiseEvent(MenuCommand command)
    {
        (command.context as TransformVariable)?.Clear();
    }
    
    [MenuItem("CONTEXT/" + nameof(TransformVariable) + "/Raise Event")]
    static void ClearSubsribers(MenuCommand command)
    {
        (command.context as TransformVariable)?.Raise();
    }

    #endregion
    
}