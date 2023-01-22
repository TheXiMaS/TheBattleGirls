using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _maxRange;
    private Vector3 _currentPos;
    private Vector3 _finalPos;

    public void SetDamage(int damage) => _damage = damage;
    
    public void SetMaxDistance(float distance) => _maxRange = distance;

    private void Start()
    {
        _finalPos = transform.right + new Vector3(_maxRange, 0, 0);
    }

    private void Update()
    {
        _currentPos = transform.position;
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (_maxRange > 0)
        {
           // Debug.Log($"{_currentPos.x} >= {_finalPos.x}");
            if (_currentPos.x >= _finalPos.x)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            collision.gameObject.GetComponent<HealthAi>().TakeDamage(_damage);
        }
        else
        {
            DamageSystem damageSystem = collision.gameObject.GetComponent<DamageSystem>();
            if (damageSystem != null)
            {
                damageSystem.TakeDamage(_damage);
                Destroy(gameObject);
            }
            else
            {
                if (collision.gameObject.CompareTag("Bullet") == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            collision.gameObject.GetComponent<HealthAi>().TakeDamage(_damage);
        }
        else
        {
            DamageSystem damageSystem = collision.gameObject.GetComponent<DamageSystem>();
            if (damageSystem != null)
            {
                damageSystem.TakeDamage(_damage);
                Destroy(gameObject);
            }
            else
            {
                if (collision.gameObject.CompareTag("Bullet") == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
