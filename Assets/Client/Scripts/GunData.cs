using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Data", menuName = "Item Data/Gun Data", order = 51)]
public class GunData : ScriptableObject
{
    [SerializeField] private string gunId;
    [SerializeField] private string gunName;
    [SerializeField] private string gunDescription;
    
    [Header("Gun Parameters")]
    [SerializeField] private int damage;
    [SerializeField] private float spread;
    [SerializeField] private float reloadTime;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float rateOfFireBurst;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxRange;
    [SerializeField] private int bulletsPerTap;
    [SerializeField] private int magazineSize;

    public string Id => gunId;
    public string Name => gunName;
    public string Description => gunDescription;

    public int Damage => damage;
    public float Spread => spread;
    public float ReloadTime => reloadTime;
    public float RateOfFire => rateOfFire;
    public float RateOfFireBurst => bulletSpeed;
    public float BulletSpeed => maxRange;
    public int BulletsPerTap => bulletsPerTap;
    public int MagazineSize => magazineSize;
}
