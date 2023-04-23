using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Properties")]
    [SerializeField] private bool isEnabled = true;
    [SerializeField] [Min(0)] private float spawnInterval = 0;
    [SerializeField] [Min(0)] private float delay = 0;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float offsetY = .5f;
    [SerializeField] private float planetarForce = 300f;

    private Collider2D _intersectsObject;

    private float _timeRemaining;
    private float _currentDelay;
    private bool _delayLeft;
    
    private GameObject _spawnedItem;

    private void Start()
    {
        if (isEnabled == false || prefab == null) return;
        
        _delayLeft = delay == 0;
        _currentDelay = delay;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void FixedUpdate()
    {
        PlanetarForcerUpdate();
    }

    private void PlanetarForcerUpdate()
    {
        if (_intersectsObject == null) return;

        var body = _intersectsObject.attachedRigidbody;
        Vector2 directionToPlanetar;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 3f);

        directionToPlanetar = (body.position - pos).normalized;
        float distance = (body.position - pos).magnitude;

        //Debug.Log("Direction: " + directionToPlanetar + ", distance: " + distance);

        body.AddForce(new Vector2(0, (planetarForce / distance)));
    }

    private void UpdateTimer()
    {
        if (prefab == null) return;

        isEnabled = _intersectsObject == null;

        if (_delayLeft == true)
        {
            if (isEnabled == false || spawnInterval == 0) return;
            
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _spawnedItem = SpawnItem();
                _timeRemaining = spawnInterval;
            }
        }
        else
        {
            StartDelay();
        }
    }
    
    private void StartDelay()
    {
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
        }
        else
        {
            _delayLeft = true;
        }
    }
    
    private GameObject SpawnItem()
    {
        var spawnPosition = new Vector2(transform.position.x, transform.position.y + offsetY);
        return Instantiate(prefab, spawnPosition, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) return;

        _intersectsObject = other;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) return;

        _intersectsObject = other;

                _intersectsObject.transform.position = Vector3.Lerp(
            new Vector2(
                _intersectsObject.transform.position.x, _intersectsObject.transform.position.y),
            new Vector2(transform.position.x, _intersectsObject.transform.position.y), 
                Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) return;

        _intersectsObject = null;

        _timeRemaining = spawnInterval;
    }
}
