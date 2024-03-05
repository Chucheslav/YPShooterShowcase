using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightningSource : MonoBehaviour
{
    [SerializeField] private float drawTime;
    [SerializeField] private float segmentLength;
    [SerializeField] private float deviation;
    [SerializeField] private bool linearMode;
    
    private LineRenderer _lineRenderer;
    private Aimer _aimer;
    private float _timer;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        _aimer = FindObjectOfType<Aimer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _aimer.hitSomething)
        {
            //дальше рисуем молнию
            Vector3 worldPoint = _aimer.WorldPoint; //точка попадания
            Vector3 sourcePoint = transform.position; //исходная точка молнии
            float length = Vector3.Distance(worldPoint, sourcePoint); //Расстояния между точками начала и конца
            int segments = Mathf.RoundToInt(length / segmentLength); //сколько всего сегментов молнии должно быть
            Vector3 direction = (worldPoint - sourcePoint).normalized; //единичный вектор, параллельный направлению от начала к концу молнии
            
            //создаем массив точек для передачи рендереру. Первая точка - источник, последняя - точка попадания
            Vector3[] points = new Vector3[segments + 1];
            points[0] = sourcePoint;
            points[segments] = worldPoint;
            
            //находим перпедникулярный направлению вектор
            Vector3 normal = Vector3.up;
            Vector3.OrthoNormalize(ref direction, ref normal);
            
            //заполняем оставшиеся точки для массива
            for (int i = 1; i < segments; i++)
            {
                // смещаем каждую точку на случайно повернуты вокруг направления на конечную точку, перпендикулярный этому направлению вектор
                if(linearMode) //линейный варинат
                    points[i] = points[0] + segmentLength * i * direction +  Quaternion.AngleAxis(Random.Range(0,360), direction) * normal * Random.Range(0,deviation);
                else // нелинейный вариант, смещает каждую точку от предыдущей
                    points[i] = points[i-1] + segmentLength * direction +  Quaternion.AngleAxis(Random.Range(0,360), direction) * normal * Random.Range(0,deviation);
            }
            
            //передаем полученный массив рендереру, включаем таймер
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
            _timer = drawTime;
        }
        else if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _lineRenderer.positionCount = 0;
        }
    }
}