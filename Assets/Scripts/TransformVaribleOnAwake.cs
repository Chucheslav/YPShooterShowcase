using UnityEngine;

public class TransformVaribleOnAwake : MonoBehaviour
{
    [SerializeField] private TransformVariable _variable;

    private void Awake()
    {
        _variable.Value = transform;
    }
}
