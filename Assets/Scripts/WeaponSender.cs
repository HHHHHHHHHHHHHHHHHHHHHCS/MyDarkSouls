using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSender : MonoBehaviour
{
    public WeaponManager weaponManager;

    public void WeaponEnable()
    {
        weaponManager.WeaponEnable();
    }

    public void WeaponDisable()
    {
        weaponManager.WeaponDisable();
    }
}
