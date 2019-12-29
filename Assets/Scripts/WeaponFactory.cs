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

        WeaponData wData = obj.AddComponent<WeaponData>();
        wData.atk = weaponDB.GetData<int>(weaponName, "ATK");

        return obj;
    }

    public GameObject CreateWeapon(string weaponName, string side, WeaponManager wm)
    {
        WeaponController wc;

        if (side == "L")
        {
            wc = wm.wcL;
        }
        else if (side == "R")
        {
            wc = wm.wcR;
        }
        else
        {
            return null;
        }

        GameObject prefab = Resources.Load<GameObject>(weaponName);
        var obj = Object.Instantiate(prefab, wc.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        WeaponData wData = obj.AddComponent<WeaponData>();
        wData.atk = weaponDB.GetData<int>(weaponName, "ATK");

        return prefab;
    }

    public bool CreateWeaponUpdate(string weaponName, string side, WeaponManager wm)
    {
        var go = CreateWeapon(weaponName, side, wm);

        if (go)
        {
            wm.UnloadWeapon(side);
            wm.UpdateWeaponCollider(side, go.GetComponent<Collider>());
        }

        return true;
    }
}