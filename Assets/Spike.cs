using UnityEngine;

public class Spike : Trap
{
    private new void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var health = collider.gameObject.GetComponent<HealthAI>();

            health.TakeDamage(damage);
            // get beeding
        }
    }
}
