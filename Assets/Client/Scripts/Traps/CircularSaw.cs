using UnityEngine;

public class CircularSaw : Trap
{
    private float _rotateSpeed = 5f;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * _rotateSpeed * Time.deltaTime);
    }
}