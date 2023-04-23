using UnityEngine;

public class Trap : MonoBehaviour
{   
    [SerializeField] protected int damage = 20;

    protected void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<HealthAI>().TakeDamage(damage);
        }
    }
}
