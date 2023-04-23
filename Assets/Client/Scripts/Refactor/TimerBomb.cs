using UnityEngine;

using static Rigidbody2DExtensions;

public class TimerBomb : Grenade
{
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionEffectPrefab;
    
    private bool _isActivated = false;
    
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private new void StartTimer()
    {
        _isActivated = true;
        if (_animator != null) _animator.SetBool("IsActivated", _isActivated);
    }
    
    private void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, explosionEffectPrefab.transform.rotation);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                AddExplosionForce(rb, transform.position, explosionForce, explosionRadius);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isEnabled) StartTimer();
    }
}
