using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private bool secondTeleporter;

    private bool _objectInTeleport;
    
    private GameObject _targetObject;
    
    private void Teleport()
    {
        if (_targetObject != null)
        {
            if (secondTeleporter)
            {
                _targetObject.transform.position = new Vector3(
                    teleportPoint.position.x + _targetObject.transform.localScale.x * 1.5f,
                    _targetObject.transform.position.y);
            }
            else
            {
                _targetObject.transform.position = new Vector3(
                    teleportPoint.position.x - _targetObject.transform.localScale.x * 1.5f,
                    _targetObject.transform.position.y);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _targetObject = other.gameObject;
        if (_targetObject.CompareTag("Player"))
        {
            _objectInTeleport = true;
            Teleport();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_targetObject.CompareTag("Player"))
        {
            _objectInTeleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _targetObject = other.gameObject;
        if (_targetObject.CompareTag("Player"))
        {
            _targetObject = null;
            _objectInTeleport = false;
        }
    }
}
