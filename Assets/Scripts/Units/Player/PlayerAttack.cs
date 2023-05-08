using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [Header("===== Runtime: DO NOT Assign =====")]
    [SerializeField] private bool attacking = true;
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private float currentAttackSpeed;
    
    [Header("===== Editor: Assignable =====")]
    public Transform[] attackPositions;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Player Attack Speed - TEMPORARY?
        currentAttackSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().atkSpeed;
        
        // Start Pool of Player Attack Projectile
        objectPooler = SingletonManager.Get<ObjectPooler>();
        // Might change in the future (due to "chosen" Character)
        objectPooler.CreatePool(objectPooler.playerWhipSO);
        
        // Start Attacking Coroutine
        StartCoroutine(AttackCoroutine());
    }
    
    private IEnumerator AttackCoroutine()
    {
        while (attacking)
        {
            yield return new WaitForSeconds(currentAttackSpeed);
            objectPooler.SpawnFromPool(objectPooler.playerWhipSO.pool.tag, attackPositions[0].position);
        }
        yield return null;
    }

    public void UpdateAttackSpeed(int attackSpeed)
    {
        currentAttackSpeed = attackSpeed;
    }
}
