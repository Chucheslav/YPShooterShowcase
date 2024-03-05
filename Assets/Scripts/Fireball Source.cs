using UnityEngine;

public class FireballSource : MonoBehaviour
{
    [SerializeField] private Fireball fireballPrefab;
    
    private Aimer aimer;

    private void Start()
    {
        aimer = FindObjectOfType<Aimer>(true);
    }
    private void Update()
    {
        //кешируем собственный трансформ - для скорости работы
        Transform T = transform;
        
        //вращаем пуляло в сторону цели
        if (aimer.hitSomething)
            T.LookAt(aimer.WorldPoint);
        else
            T.LookAt(T.position + T.root.forward);
        
        //создаем шарик
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fireballPrefab, T.position, T.rotation);
        }
    }
}
