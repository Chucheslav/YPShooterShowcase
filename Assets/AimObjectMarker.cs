
using UnityEngine;

public class AimObjectMarker : MonoBehaviour
{

    private Aimer _aimer;
    
    void Start()
    {
        _aimer = FindObjectOfType<Aimer>(true);
    }
    
    void Update()
    {
        if(!_aimer) return;

        transform.position = _aimer.WorldPoint;
    }
}
