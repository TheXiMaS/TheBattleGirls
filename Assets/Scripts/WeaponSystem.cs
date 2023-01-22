using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenShooting, reloadTime, spread, range, timeBetweenShoots, bulletSpeed, recoil;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private FireMode _currentFireMode = FireMode.Single;
    [SerializeField] private bool allowChangeMode = false;
    
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;

    [Header("Audio Settings")]
    [SerializeField] private GameObject fxManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI fireModeText;
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Player")]
    [SerializeField] private GameObject player;

    private int _bulletsLeft, _bulletsShot;

    private int _currentBulletsPerTap;

    private bool _shooting, _readyToShoot, _reloading;

    private void Awake()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
        _currentBulletsPerTap = bulletsPerTap;

        switch (_weaponType)
        {
            case WeaponType.Pistol:
                _currentFireMode = FireMode.Single;
                break;

            case WeaponType.DMR:
                _currentFireMode = FireMode.Single;
                break;

            case WeaponType.SR:
                _currentFireMode = FireMode.Single;
                break;

            case WeaponType.Shotgun:
                _currentFireMode = FireMode.Single;
                break;

            case WeaponType.AR:
                _currentFireMode = FireMode.Auto;
                break;
        }

        if (fireModeText != null)
            fireModeText.text = _currentFireMode.ToString().ToUpper();

        if (ammoText != null)
            ammoText.text = $"{_bulletsLeft} / {magazineSize}";
    }

    private void Update()
    {
        UpdateFire();

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < magazineSize && _reloading == false)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_weaponType == WeaponType.AR)
            {
                ChangeFireMode();
            }
        }

        if (_readyToShoot == true && _shooting == true && _reloading == false && _bulletsLeft > 0)
        {
            _bulletsShot = _currentBulletsPerTap;
            Shoot();
        }
    }

    private void UpdateFire()
    {
        switch (_currentFireMode)
        {
            case FireMode.Auto:
                _shooting = Input.GetButton("Fire1");
                break;

            case FireMode.Burst:
                _shooting = Input.GetButtonDown("Fire1");
                break;

            case FireMode.Single:
                _shooting = Input.GetButtonDown("Fire1");
                break;

            default:
                _currentFireMode = FireMode.Auto;
                _shooting = Input.GetButton("Fire1");
                break;
        }
    }

    private void ChangeFireMode()
    {
        if (allowChangeMode == true)
        {
            switch (_currentFireMode)
            {
                case FireMode.Auto:
                    _currentFireMode = FireMode.Single;
                    _currentBulletsPerTap = bulletsPerTap;
                    break;

                case FireMode.Single:
                    _currentFireMode = FireMode.Burst;
                    _currentBulletsPerTap = 3;
                    break;

                case FireMode.Burst:
                    _currentFireMode = FireMode.Auto;
                    _currentBulletsPerTap = bulletsPerTap;
                    break;

                default:
                    _currentFireMode = FireMode.Auto;
                    _currentBulletsPerTap = bulletsPerTap;
                    break;
            }
            fireModeText.text = _currentFireMode.ToString().ToUpper();
        }
    }

    private void Shoot()
    {
        _readyToShoot = false;

        ReleaseBullet();

        player.GetComponent<PlayerMovement>().TakeRecoil(recoil);

        _bulletsLeft--;
        _bulletsShot--;

        if (IsInvoking(nameof(ResetShot)) == false && _readyToShoot == false)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
        }

        if (_bulletsShot > 0 && _bulletsLeft > 0)
        {
            Shoot();
        }


        ammoText.text = $"{_bulletsLeft} / {magazineSize}";
    }

    private void ResetShot()
    {
        _readyToShoot = true;
    }

    private void Reload()
    {
        ammoText.text = "Reloading...";
        ammoText.fontSize = 36;
        
        _reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;

        ammoText.text = $"{_bulletsLeft} / {magazineSize}";
        ammoText.fontSize = 46;
    }

    private void ReleaseBullet()
    {
        float y = Random.Range(-spread, spread);
        Vector3 dir = firePoint.transform.right + new Vector3(0, y, 0);

        GameObject bulletIns = Instantiate(bullet, firePoint.position, firePoint.rotation);

        bulletIns.GetComponent<Bullet>().SetDamage(damage);
        bulletIns.GetComponent<Bullet>().SetMaxDistance(range);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed);
    }

    private enum FireMode
    {
        Single,
        Burst,
        Auto
    }

    private enum WeaponType
    {
        Pistol,
        Shotgun,
        AR,
        DMR,
        SR
    }
}
