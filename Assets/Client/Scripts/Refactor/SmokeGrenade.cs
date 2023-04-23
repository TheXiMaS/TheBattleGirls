using UnityEngine;

public class SmokeGrenade : Grenade
{
    [SerializeField] private GameObject smokeEffectPrefab;

    private Smoke _smoke;

    private void Start()
    {
        Invoke(nameof(Launch), launchDelay);
    }

    private void Launch()
    {
        Instantiate(smokeEffectPrefab, transform.position, smokeEffectPrefab.transform.rotation);

        Destroy(gameObject);
    }
}
