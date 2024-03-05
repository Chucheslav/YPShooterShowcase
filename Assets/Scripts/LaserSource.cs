using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSource : MonoBehaviour
{

    private LineRenderer _lineRenderer;
    private Aimer _aimer;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _aimer = FindObjectOfType<Aimer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && _aimer.hitSomething)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new[] {transform.position, _aimer.WorldPoint});
        }
        else _lineRenderer.positionCount = 0;
    }
}