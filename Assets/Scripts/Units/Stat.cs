using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

public class Stat : MonoBehaviour
{
    public Health unitHealth = new (100, 100);

    [Header(DS_Constants.DO_NOT_ASSIGN)]
    private float additionMultiplier = 300;
    private float powerMultipler = 2;
    private float divisionMultiplier = 7;
    private Coroutine dotCoroutine;
    private SimpleFlash simpleFlash;

    public int level;
    public float currentXP;
    public float requiredXP;
    public float atkSpeed;
    public float damage;
    public float moveSpeed;

    [Header(DS_Constants.ASSIGNABLE)]
    public SO_Stat statSO;

    public virtual void Start()
    {
        unitHealth.onDeathEvent.AddListener(Death);
        simpleFlash = GetComponentInChildren<SimpleFlash>();
    }

    protected virtual void OnEnable()
    {
        unitHealth.health = statSO.health;
        unitHealth.maxHealth = statSO.maxHealth;
        atkSpeed = statSO.atkSpeed;
        damage = statSO.damage;
        moveSpeed = statSO.moveSpeed;
    }
    
    protected virtual void OnDisable()
    {
        
    }
    
    public virtual void TakeDamage(float amount, float multiplier)
    {
        simpleFlash.Flash();
        unitHealth.Damage(amount, multiplier);
    }

    private IEnumerator TakeDamageOverTime(float damage, float multiplier, float time)
    {
        // Loop this
        while (true)
        {
            TakeDamage(damage, multiplier);
            yield return new WaitForSeconds(time);
        }
    }

    public void StartDoT(float damage, float multiplier, float time)
    {
        if (gameObject.activeInHierarchy)
        {
            // Damage Over Time Animation?
            
            dotCoroutine = StartCoroutine(TakeDamageOverTime(damage, multiplier, time));
            //Debug.Log(dotCoroutine + " start!");
        }
    }

    public void StopDoT()
    {
        // Stop Damage Over Time Animation
        
        StopAllCoroutines();
    }

    public void DecrementMoveSpeed(float amount)
    {
        float temp = moveSpeed * amount;
        moveSpeed -= temp;
        GetComponent<EnemyMovement>().UpdateMovementSpeed(moveSpeed);
    }

    public void ResetMoveSpeed()
    {
        moveSpeed = statSO.moveSpeed;
        GetComponent<EnemyMovement>().UpdateMovementSpeed(moveSpeed);
    }
    
    protected virtual void Death()
    {
        gameObject.SetActive(false);
        unitHealth.health = statSO.health;
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

    public virtual void LevelUp()
    {
        level++;
        // frontXPBar.fillAmount = 0f;
        // backXpBar.fillAmount = 0f;

        currentXP = Mathf.RoundToInt((currentXP - requiredXP));
        unitHealth.IncreaseHealth(level);
        requiredXP = CalculateRequiredXP();
    }

    protected int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXP += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultipler,levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}
