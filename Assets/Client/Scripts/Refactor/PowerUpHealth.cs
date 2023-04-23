using UnityEngine;

public class PowerUpHealth : PowerUp
{
    [SerializeField] [Min(5)] private int bonus = 50;

    private void Update() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                var health = collider.GetComponent<HealthAI>();
                
                if (health == null) return;

                health.TakeHealth(bonus);
                Destroy(gameObject);
            }
        }
    }
}