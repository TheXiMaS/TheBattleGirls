using JetBrains.Annotations;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenShooting, reloadTime, spread, range, timeBetweenShoots, bulletSpeed, recoil;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;

    [Header("Bullet System")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RaycastHit2D rayHit;
    [SerializeField] private LayerMask whatIsEnemy;

    [SerializeField] private PlayerMovement player;

    private int _bulletsLeft, _bulletsShot;

    private bool _shooting, _readyToShoot, _reloading;

    private void Awake()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
    }

    private void Update()
    {
        if (allowButtonHold)
        {
            _shooting = Input.GetButton("Fire1");
        }
        else
        {
            _shooting = Input.GetButtonDown("Fire1");
        }

        if (Input.GetKeyDown(KeyCode.R) && _bulletsLeft < magazineSize && _reloading == false)
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.B) && _reloading == false)
        {
            if (allowButtonHold == true)
            {
                allowButtonHold = false;
                Debug.Log("Single");
            }
            else
            {
                allowButtonHold = true;
                Debug.Log("Auto");
            }
        }

        if (_readyToShoot == true && _shooting == true && _reloading == false && _bulletsLeft > 0)
        {
            _bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        _readyToShoot = false;

        float y = Random.Range(-spread, spread);

        Vector3 dir = firePoint.transform.right + new Vector3(0, y, 0);
        
        player.TakeRecoil(recoil);

        GameObject bulletIns = Instantiate(bullet, firePoint.position, firePoint.rotation);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed);

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


        Debug.Log($"{_bulletsLeft} / {magazineSize}");
    }

    private void ResetShot()
    {
        _readyToShoot = true;
    }

    private void Reload()
    {
        Debug.Log($"Reloading...");
        
        _reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        Debug.Log($"Ready!");

        _bulletsLeft = magazineSize;
        _reloading = false;
    }
}
