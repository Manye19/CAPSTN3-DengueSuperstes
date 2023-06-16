using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Stat : MonoBehaviour
{
    public Health unitHealth = new Health(100, 100);

    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float additionMultiplier = 300;
    private float powerMultipler = 2;
    private float divisionMultiplier = 7;
    private Coroutine dotCoroutine;
    
    [Header(DS_Constants.ASSIGNABLE)] 
    [Header("Stats")]
    public int level;
    public float currentXP;
    public float requiredXP;
    public int health;
    public int maxHealth;
    public float atkSpeed;
    public float damage;
    public int defense;
    public float movementSpeed;

    public virtual void Start()
    {
        unitHealth.health = health;
        unitHealth.maxHealth = maxHealth;
        requiredXP = CalculateRequiredXP();
    }

    private void OnEnable()
    {
        unitHealth.onDeathEvent.AddListener(Death);
    }
    
    private void OnDisable()
    {
        unitHealth.onDeathEvent.RemoveListener(Death);
    }
    
    public void TakeDamage(float amount)
    {
        //Debug.Log("Damage!");
        unitHealth.Damage(amount);
    }

    private IEnumerator TakeDamageOverTime(float damage, float time)
    {
        // Loop this
        while (true)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(time);
        }
    }

    public void StartDoT(float damage, float time)
    {
        if (gameObject.activeInHierarchy)
        {
            dotCoroutine = StartCoroutine(TakeDamageOverTime(damage, time));
        }
        //Debug.Log(dotCoroutine + " start!");
    }

    public void StopDoT()
    {
        if (dotCoroutine != null)
        {
            StopCoroutine(dotCoroutine);
            //Debug.Log(dotCoroutine + " stopped.");
        }
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
    }

    public void Heal(float amount)
    {
        unitHealth.Heal(amount);
    }

    public virtual void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        // lerpTimer = 0f;
        if (currentXP >= requiredXP)
        {
            LevelUp();
        }
    }

    protected virtual void GainExperienceScalable(float xpGained, int playerLevel, int enemyLevel)
    {
        float multiplier = 1;
        if (playerLevel < enemyLevel)
        {
            multiplier = 1 + (enemyLevel - playerLevel) * 0.1f;
            currentXP += xpGained * multiplier;
        }
        else
        {
            currentXP += xpGained;
        }
        if (currentXP >= requiredXP)
        {
            LevelUp();
        }
        Debug.Log("Player gained " + (xpGained * multiplier) + " EXP.");
        // lerpTimer = 0f;
    }

    protected virtual void LevelUp()
    {
        level++;
        // frontXPBar.fillAmount = 0f;
        // backXpBar.fillAmount = 0f;
        
        
        currentXP = Mathf.RoundToInt((currentXP - requiredXP));
        unitHealth.IncreaseHealth(level);
        requiredXP = CalculateRequiredXP();
    }

    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXP += (int)Mathf.Floor(levelCycle +
                                                   additionMultiplier * Mathf.Pow(powerMultipler,
                                                       levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}
