using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMsgSender : MonoBehaviour
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
