using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Gun : Weapon
{
    [SerializeField] private float spread;
    [SerializeField] private float reloadTime;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float rateOfFireBurst;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxRange;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private int magazineSize;
    
    [Header("Fire Mode Parameters")]
    [SerializeField] private bool canChangeFireMode;
    [Space]
    [SerializeField] private bool singleMode = true;
    [SerializeField] private bool burstMode = false;
    [SerializeField] private bool autoMode = false;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;

    private bool _canReload = true;
    private FireMode _currentFireMode;
    private Dictionary<FireMode, bool> _allowedFireModes;

    private int _currentBulletsPerTap;
    
    private bool _reloadButton, _fireModeButton;
    private bool _fireButton, _fireButtonDown;

    private int _bulletsLeft, _bulletsShot, _burstBulletsLeft;

    private bool _shooting, _readyToShoot, _reloading, _shootingBurst;

    private bool _inHand;

    private void Start()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
        CheckInventory();
        _currentBulletsPerTap = bulletsPerTap;
        SetStartFireMode();
    }

    private void Update()
    {
        if (_inHand)
        {
            GetInputValues();

            if (_canReload && _reloadButton && _bulletsLeft < magazineSize && _reloading == false)
            {
                Reload();
            }
            
            if (canChangeFireMode && _fireModeButton)
            {
                ChangeFireMode();
            }

            if (_readyToShoot && _shooting && _reloading == false && _bulletsLeft > 0)
            {
                _bulletsShot = _currentBulletsPerTap;
                Shoot();
            }
        }

        CheckInventory();
    }

    
    // Нужно отрефакторить этот метод и избавиться от мусора
    private void SetStartFireMode()
    {
        _allowedFireModes = new Dictionary<FireMode, bool>
        {
            { FireMode.Single, singleMode },
            { FireMode.Burst, burstMode },
            { FireMode.Auto, autoMode }
        };

        var allowedCount = 0;

        foreach (var fireMode in _allowedFireModes)
        {
            if (fireMode.Value == true)
            {
                allowedCount++;
            }
        }

        if (allowedCount <= 1)
        {
            canChangeFireMode = false;
        }

        if (autoMode == true)
        {
            _currentFireMode = FireMode.Auto;
        }
        else if (burstMode == true)
        {
            _currentFireMode = FireMode.Burst;
        }
        else
        {
            _currentFireMode = FireMode.Single;
        }

        if (_currentFireMode == FireMode.Burst)
        {
            if (bulletsPerTap > 1)
            {
                _currentBulletsPerTap = bulletsPerTap;
            }
            else
            {
                _currentBulletsPerTap = 3;
            } 
        }
        else
        {
            _currentBulletsPerTap = bulletsPerTap;
        }
    }

    
    // Метод апдейтит все инпуты
    private void GetInputValues()
    {
        _reloadButton = Input.GetButtonDown(Button.Reload);
        _fireModeButton = Input.GetButtonDown(Button.FireMode);

        _fireButton = Input.GetButton(Button.Fire);
        _fireButtonDown = Input.GetButtonDown(Button.Fire);

        _shooting = _currentFireMode == FireMode.Auto ? _fireButton : _fireButtonDown;
    }

    
    // Инвокающийся метод выстрела (Можно заменить на таймер Time.deltaTime)
    private void Shoot()
    {
        _readyToShoot = false;

        ReleaseBullet();

        _bulletsLeft--;
        _bulletsShot--;

        if (IsInvoking(nameof(ResetShot)) == false && _readyToShoot == false)
        {
            Invoke(nameof(ResetShot), rateOfFire);
        }

        if (_bulletsShot > 0 && _bulletsLeft > 0)
        {
            Shoot();
        }
    }

    // Сброс готовности к выстрелу
    private void ResetShot() => _readyToShoot = true;

    // Перезарядка, инвокает метод окончания перезарядки
    private void Reload()
    {
        _reloading = true;

        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;

        Debug.Log($"{_bulletsLeft} / {magazineSize}");
    }

    // Выпускает пулю
    private void ReleaseBullet()
    {
        var spreadY = Random.Range(-spread, spread);
        var dir = firePoint.transform.right + new Vector3(0, spreadY, 0);
        var bulletIns = Instantiate(bullet, firePoint.position, firePoint.rotation);

        bulletIns.GetComponent<Bullet>().SetDamage(damage);
        bulletIns.GetComponent<Bullet>().SetDistance(maxRange / bulletSpeed);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed);
    }

    private void CheckInventory()
    {
        _inHand = gameObject.transform.parent != null && gameObject.transform.parent.name == "Inventory";
    }

    // Изменяет режим стрельбы
    private void ChangeFireMode()
    {
        switch(_currentFireMode)
        {
            case FireMode.Single:
                if (burstMode)
                {
                    _currentFireMode = FireMode.Burst;
                }
                else if (autoMode)
                {
                    _currentFireMode = FireMode.Auto;
                }
                break;

            case FireMode.Burst:
                if (autoMode)
                {
                    _currentFireMode = FireMode.Auto;
                }
                else if (singleMode)
                {
                    _currentFireMode = FireMode.Single;
                }
                break;

            case FireMode.Auto:
                if (singleMode)
                {
                    _currentFireMode = FireMode.Single;
                }
                else if (burstMode)
                {
                    _currentFireMode = FireMode.Burst;
                }
                break;
        }

        if (_currentFireMode == FireMode.Burst)
        {
            _currentBulletsPerTap = 3;
        }
        else
        {
            _currentBulletsPerTap = 1;
        }
    }

    public enum FireMode
    {
        Single,
        Burst,
        Auto
    }

    public int GetBulletsLeft() { return _bulletsLeft; }
    public int GetMagazineSize() { return magazineSize; }
    public bool IsReloading() { return _reloading; }

    public FireMode GetCurrentFireMode() { return _currentFireMode; }
}