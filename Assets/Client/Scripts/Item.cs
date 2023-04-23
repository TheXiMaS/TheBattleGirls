using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    /*----------------------------------------------------*
     *                                                    *
     * ID =  1   01  01  001                              *
     *      ||   ||  ||   ||                              *
     *      1)   2)  3)   4)                              *
     *                                                    *
     * 1) Interaction method (One time use / Usability)   *
     * 2) Item group (Powerup/Weapon)                     *
     * 3) Item class (Weapon[Melee class])                *
     * 4) Item number                                     *
     *                                                    *
     *----------------------------------------------------*/                                                    

    [SerializeField] protected string _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;

    public string GetId() => _id;
    public string GetName() => _name;
    public string GetDescription() => _description;
}
