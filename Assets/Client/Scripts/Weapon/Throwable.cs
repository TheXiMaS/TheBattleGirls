using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon
{
    private float _explosiveForce;
    private float _timeToExplosive;

    Throwable()
    {
        _explosiveForce = 300f;
        _timeToExplosive = 5f;
    }
}