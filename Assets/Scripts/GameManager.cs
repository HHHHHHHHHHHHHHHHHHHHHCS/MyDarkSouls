using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        CheckGameObject();
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
    }
}