using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class WeaponDataBase
{
    private string weaponDataBaseFileName = "WeaponData";

    private JObject weaponData;

    public JObject WeaponData => weaponData;


    public WeaponDataBase()
    {
        var text = Resources.Load<TextAsset>(weaponDataBaseFileName);
        weaponData = JObject.Parse(text.text);
    }

    public T GetData<T>(string name, string id)
    {
        return WeaponData[name][id].Value<T>();
    }
}