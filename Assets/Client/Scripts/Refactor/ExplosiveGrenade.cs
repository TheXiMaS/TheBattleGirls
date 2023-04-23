using UnityEngine;

using static Rigidbody2DExtensions;

public class ExplosiveGrenade : Grenade
{
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject explosionEffectPrefab;

    private void Update()
    {
        if (_timeLeft == false) TimerUpdate();
        if (_timeLeft) Explode();
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        StartTimer();
    }
}
