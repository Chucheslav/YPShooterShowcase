using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] private Vector3 screenPoint = new Vector3(0.5f, 0.65f, 0);
    [SerializeField] private float maxRange = 100;
    
    public Vector3 WorldPoint { get; private set; }
    public Vector3 ScreenPoint { get; private set; }
    public bool hitSomething;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        var ray = _camera.ViewportPointToRay(screenPoint);
        hitSomething = Physics.Raycast(ray, out RaycastHit hit, maxRange);
        if (hitSomething)
        {
            WorldPoint = hit.point;
            ScreenPoint = _camera.WorldToScreenPoint(hit.point);
        }

        else ScreenPoint = new Vector3(_camera.pixelWidth * screenPoint.x, _camera.pixelHeight * screenPoint.y, 0);

    }
}
