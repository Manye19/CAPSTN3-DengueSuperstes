using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float startTime;
    private bool timerActive = false;

    [Header(DS_Constants.ASSIGNABLE)]
    public GameObject player;
    public List<Transform> objectiveTransformsList;
    public List<Transform> itemTransformsList;
    public List<Transform> obstacleTransformsList;

    public OnLevelUpEvent onLevelUpEvent = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void OnEnable()
    {
        onLevelUpEvent.AddListener(PauseGameTime);
    }

    private void OnDisable()
    {
        onLevelUpEvent.RemoveListener(PauseGameTime);
    }

    private void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        SingletonManager.Get<UIManager>().timerText.text = minutes + ":" + seconds;
    }

    public Transform GetRandTransformFromList(List<Transform> list)
    {
        int randNum = Random.Range(0, list.Count);
        return list[randNum];
    }

    public void PauseGameTime(bool p_bool)
    {
        Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }
}
