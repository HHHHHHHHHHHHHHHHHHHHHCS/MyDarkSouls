﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager weaponManager;
    public WeaponData weaponData;

    private void Awake()
    {
        weaponData = GetComponentInChildren<WeaponData>();
    }
}
