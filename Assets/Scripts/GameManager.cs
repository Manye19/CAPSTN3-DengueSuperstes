using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
}
