using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private int touchDamage = 20;
    [SerializeField] private int burningDamage = 2;

    private float _burnTime = 5;
    private bool _inFire = false;
    private bool _isBurning = false;

    private GameObject _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _target = collision.gameObject;
            _isBurning = true;
            StartCoroutine(DamageFromBurning());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_inFire == false)
            {
                StartCoroutine(TouchDamage());
                _inFire = true;
            }
            
            if (_isBurning == false)
            {
                StartCoroutine(DamageFromBurning());
                _isBurning = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inFire = false;
        }
    }

    private IEnumerator DamageFromBurning()
    {
        for (int i = 0; i < _burnTime; i++)
        {
            yield return new WaitForSeconds(.25f);
            _target.GetComponent<HealthAi>().TakeDamage(burningDamage);
        }
        _isBurning = false;
        yield return null;
    }

    private IEnumerator TouchDamage()
    {
        _target.GetComponent<HealthAi>().TakeDamage(touchDamage);
        yield return new WaitForSeconds(1);
        _inFire = false;
        yield return null;
    }
}
