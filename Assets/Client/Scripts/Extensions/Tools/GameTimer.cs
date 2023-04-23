using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TimerType timerType = TimerType.SecondsToSpawn;
    [SerializeField] private GameObject prefab;
    [SerializeField][Min(0)] private float delay = 3;
    [SerializeField][Min(0)] private float firstDelay = 0;
    [SerializeField] private bool offsetRandomRange = false;
    [SerializeField] private Vector2 spawnOffset;

    private float _timeRemaining;
    private float _currentDelay;
    private bool _delayLeft;

    private GameObject _spawnedObject;

    private void Start()
    {
        if (firstDelay == 0)
        {
            _delayLeft = delay == 0;
            _currentDelay = delay;
        }
        else
        {
            _delayLeft = firstDelay == 0;
            _currentDelay = firstDelay;
        }
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (timerType == TimerType.SecondsToSpawn)
        {
            if (_delayLeft == true)
            {
                if (_timeRemaining > 0)
                {
                    _timeRemaining -= Time.deltaTime;
                }
                else
                {
                    _spawnedObject = SpawnSun();
                    _timeRemaining = delay;
                }
            }
            else
            {
                StartDelay();
            }
        }
        else if (timerType == TimerType.SpawnPerSecond)
        {
            if (Time.time >= _timeRemaining)
            {
                _spawnedObject = SpawnSun();
                _timeRemaining = Time.time + 1f / delay;
            }
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

    private GameObject SpawnSun()
    {
        var offsetPosition = new Vector2(spawnOffset.x, spawnOffset.y);
        var spawnPosition = new Vector2(transform.position.x, transform.position.y)
            + offsetPosition;

        return Instantiate(prefab, spawnPosition, prefab.transform.rotation);
    }

    public GameObject GetSpawnedObject()
    {
        return _spawnedObject;
    }

    private enum TimerType
    {
        SecondsToSpawn,
        SpawnPerSecond
    }
}
