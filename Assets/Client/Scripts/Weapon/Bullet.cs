using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _range;

    private void Start()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Item");
            
        if (target.GetComponent<Collider2D>() != null)
            Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
        Destroy(gameObject, _range);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Bullet")) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out HealthAI health))
            {
                health.TakeDamage(_damage);
            }
        }

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage) => _damage = damage;
    public void SetDistance(float value) => _range = value;
}
