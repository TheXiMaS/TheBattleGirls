using System.Collections.Generic;
using UnityEngine;

public class PlanetarForcer : MonoBehaviour
{
    [SerializeField] private float force = 30f;
    [SerializeField] private PlanetarType planetarType = PlanetarType.Circle;
    private HashSet<Rigidbody2D> affectedBodies = new HashSet<Rigidbody2D>();

    private void FixedUpdate()
    {
        foreach (Rigidbody2D body in affectedBodies)
        {
            Vector2 directionToPlanetar;

            if (planetarType == PlanetarType.Circle)
            {
                directionToPlanetar = (body.position - 
                    new Vector2(transform.position.x, transform.position.y)).normalized;
            }
            else
            {
                directionToPlanetar = transform.up;
            }

            float distance = (body.position - 
                new Vector2(transform.position.x, transform.position.y)).magnitude;

            body.AddForce((force / distance) * directionToPlanetar);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.attachedRigidbody != null)
        {
            affectedBodies.Add(collider.attachedRigidbody);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.attachedRigidbody != null)
        {
            affectedBodies.Remove(collider.attachedRigidbody);
        }
    }

    private enum PlanetarType 
    {
        Circle,
        Directional
    }
}
