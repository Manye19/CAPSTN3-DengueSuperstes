using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public Vector3 offset;
    private void Start()
    {
        player = SingletonManager.Get<GameManager>().player;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
