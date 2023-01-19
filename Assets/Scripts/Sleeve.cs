using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeve : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float balistics = 4f;

    private int _lifeTime = 2;

    private void Start()
    {
        //GetComponent<Rigidbody2D>().gravityScale = balistics;
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        StartCoroutine(DestroySleeve());
    }

    private IEnumerator DestroySleeve()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
