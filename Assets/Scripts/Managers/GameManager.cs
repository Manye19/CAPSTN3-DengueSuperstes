using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float startTime;
    private bool timerActive = false;

    [Header(DS_Constants.ASSIGNABLE)]
    public GameObject player;
    public GameObject objectivePrefab;
    public List<Transform> objectiveTransformsList;
    public List<Transform> itemTransformsList;
    public List<Transform> obstacleTransformsList;

    public OnLevelUpEvent onLevelUpEvent = new();
    public OnDeathEvent onPlayerDeath = new();
    
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Time.timeScale = 1;
        startTime = Time.time;

        foreach (Transform transform in objectiveTransformsList)
        {
            GameObject go = Instantiate(objectivePrefab, transform);
            go.transform.SetParent(SingletonManager.Get<ObjectPooler>().transform);
        }
    }

    private void OnEnable()
    {
        onLevelUpEvent.AddListener(PauseGameTime);
        onPlayerDeath.AddListener(PauseOnPlayerDeath);
    }

    private void OnDisable()
    {
        onLevelUpEvent.RemoveListener(PauseGameTime);
        onPlayerDeath.RemoveListener(PauseOnPlayerDeath);
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
        Time.timeScale = p_bool ? 0 : 1;
        //Time.timeScale = Time.timeScale >= 1 ? 0 : 1;
    }

    public void PauseOnPlayerDeath()
    {
        Time.timeScale = 0;
    }

    public void GameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
