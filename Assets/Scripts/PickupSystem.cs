using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private GameObject weapon;

    private bool _slotIsEmpty = true;

    private void Update()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (_slotIsEmpty == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Instantiate(weapon, weaponPoint.position, weaponPoint.rotation);
                weapon.transform.SetParent(gameObject.transform);
                _slotIsEmpty = false;
            }
        }
    }
}
