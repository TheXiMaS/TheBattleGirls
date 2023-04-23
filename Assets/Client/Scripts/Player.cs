using UnityEngine;

public class Player : MonoBehaviour
{
    private FirstPlayerController _movementController;
    private HealthAI _health;

    private void Awake()
    {
        _health = GetComponent<HealthAI>();

        _movementController = GetComponent<FirstPlayerController>();

        if (_movementController == null)
            Debug.LogWarning("<MovementController> is not initialized!");

        if (_health == null)
            Debug.LogWarning("<Health> is not initialized!");
    }
}
