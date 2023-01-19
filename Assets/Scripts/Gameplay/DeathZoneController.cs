using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    [SerializeField] private int damage = 999;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().SetDeath();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
