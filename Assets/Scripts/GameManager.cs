using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private WeaponDataBase weaponDataBase;

    private void Awake()
    {
        CheckGameObject();
        CheckSingle();
        InitWeaponDB();
    }

    private void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void CheckGameObject()
    {
        if (!gameObject.CompareTag("GM"))
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //var prefab = Resources.Load<GameObject>("Falchion");
        //Instantiate(prefab);
        //var text = Resources.Load<TextAsset>("ABC");
        //var obj = JObject.Parse(text.text);
        //print(obj["Falchion"]["ATK"].Value<string>());
    }

    private void InitWeaponDB()
    {
        weaponDataBase = new WeaponDataBase();
    }
}