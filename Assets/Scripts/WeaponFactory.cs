using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private WeaponDataBase weaponDB;

    public WeaponFactory(WeaponDataBase _weaponDB)
    {
        weaponDB = _weaponDB;
    }

    public void CreateWeapon(string weaponName)
    {
        GameObject prefab = Resources.Load<GameObject>(weaponName);
        Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject CreateWeapon(string weaponName, Vector3 pos, Quaternion rot)
    {
        GameObject prefab = Resources.Load<GameObject>(weaponName);
        var obj = Object.Instantiate(prefab, pos, rot);
        return obj;
    }

    public GameObject CreateWeapon(string weaponName, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(weaponName);
        var obj = Object.Instantiate(prefab);
        obj.transform.parent = parent;
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        return obj;
    }
}