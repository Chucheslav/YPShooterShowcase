using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private float damage = 10;

    private float _timer;

    private void Awake()
    {
        _timer = lifetime;
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        if(_timer <= 0) DestroyFireball();
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.root.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);            
        }
        DestroyFireball();
    }


    private void DestroyFireball()
    {
        Destroy(gameObject);
    }      
}