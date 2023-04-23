using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rigidbody2DExtensions
{
    public static void AddExplosionForce(Rigidbody2D rb, Vector3 explosionPosition, float explosionForce, float explosionRadius)
    {
        Vector3 direction = rb.transform.position - explosionPosition;
        float forceFalloff = 1 - (direction.magnitude / explosionRadius);
        rb.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff, ForceMode2D.Force);
        rb.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff, ForceMode2D.Impulse);
    }
}
