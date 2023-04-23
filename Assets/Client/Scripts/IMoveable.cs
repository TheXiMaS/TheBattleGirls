using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public void MoveTo(Vector2 direction, float speed);
    public void MoveLeft(float speed);
    public void MoveRight(float speed);
    public void MoveUp(float speed);
    public void MoveDown(float speed);
}
