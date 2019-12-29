using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;


    public WeaponManager playerWeaponManger;

    private WeaponDataBase weaponDataBase;
    private WeaponFactory weaponFactory;

    private void Awake()
    {
        CheckGameObject();
        CheckSingle();

        if (Instance == this)
        {
            Cursor.lockState = CursorLockMode.Locked; //hide mouse cursor 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        }
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

        InitWeaponDB();
        InitWeaponFactory();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 300, 60), "R: Sword"))
            weaponFactory.CreateWeaponUpdate("Sword", "R", playerWeaponManger);
        if (GUI.Button(new Rect(10, 70, 300, 60), "R: Falchion"))
            weaponFactory.CreateWeaponUpdate("Falchion", "R", playerWeaponManger);
        if (GUI.Button(new Rect(10, 130, 300, 60), "R: Mace"))
            weaponFactory.CreateWeaponUpdate("Mace", "R", playerWeaponManger);
    }


    private void InitWeaponDB()
    {
        weaponDataBase = new WeaponDataBase();
    }

    private void InitWeaponFactory()
    {
        weaponFactory = new WeaponFactory(weaponDataBase);
    }
}